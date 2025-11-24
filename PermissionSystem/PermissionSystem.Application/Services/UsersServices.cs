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

    public async Task<UserDTO?> CreateUser(UserDTO user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return null;

        if (!await _context.Groups.AnyAsync(x => user.Groups.Select(x => x.Id).Contains(x.Id)))
            return null;

        User entity = _mapper.Map<User>(user);

        entity.System = null;
        entity.GroupUsers = null;
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        foreach (var group in user.Groups!)
        {
            GroupUser groupUser = new()
            {
                GroupId = group.Id,
                UserId = entity.Id
            };
            _context.GroupUsers.Add(groupUser);
            await _context.SaveChangesAsync();
        }

        return user;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _context.Users
        .Include(u => u.System)
        .Include(u => u.GroupUsers)
            .ThenInclude(gu => gu.Group)
        .ToListAsync();

        var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

        return usersDto;
    }
}
