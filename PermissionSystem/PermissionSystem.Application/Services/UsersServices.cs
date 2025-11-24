using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Services;

public class UsersServices
{
    private readonly PermissionSystemContext _context;
    private readonly IMapper _mapper;

    public UsersServices(PermissionSystemContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDTO?> CreateUser(UserCreateDTO userCreateDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == userCreateDto.Email))
            return null;

        if (!await _context.Groups.AnyAsync(x => userCreateDto.GroupIds!.Contains(x.Id)))
            return null;

        User entity = _mapper.Map<User>(userCreateDto);

        entity.System = null;
        entity.GroupUsers = null;
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        foreach (var groupId in userCreateDto.GroupIds!)
        {
            GroupUser groupUser = new()
            {
                GroupId = groupId,
                UserId = entity.Id
            };
            _context.GroupUsers.Add(groupUser);
            await _context.SaveChangesAsync();
        }

        return _mapper.Map<UserDTO>(entity);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.GroupUsers)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null || user.GroupUsers == null)
            return false;

        user.GroupUsers.Clear();

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _context.Users
            .Include(u => u.System)
            .Include(u => u.GroupUsers!)
                .ThenInclude(gu => gu.Group)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO?> GetUserById(int userId)
    {
        var user = await _context.Users
            .Include(u => u.System)
            .Include(u => u.GroupUsers!)
                .ThenInclude(gu => gu.Group)
            .FirstOrDefaultAsync(x => x.Id.Equals(userId));

        if (user == null)
            return null;

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO?> UpdateUser(int userId, UserUpdateDTO dto)
    {
        var user = await _context.Users
            .Include(u => u.GroupUsers)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));

        if (user == null)
            return null;

        user.Name = dto.Name!;
        user.Email = dto.Email!;

        if (dto.SystemId != null)
            user.SystemId = dto.SystemId.Value;

        if (!string.IsNullOrEmpty(dto.Password))
            user.Password = dto.Password;

        if (dto.GroupIds != null)
        {
            user.GroupUsers!.Clear();

            foreach (var groupId in dto.GroupIds)
            {
                user.GroupUsers.Add(new GroupUser { GroupId = groupId, UserId = user.Id });
            }
        }

        await _context.SaveChangesAsync();

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<IEnumerable<UserDTO>> GetUsersBySystem(int id)
    {
        var users = await _context.Users.Where(u => u.SystemId == id)
            .Include(u => u.System)
            .Include(u => u.GroupUsers!)
            .ThenInclude(gu => gu.Group).ToListAsync();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

}
