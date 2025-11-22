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

        User entity = _mapper.Map<User>(user);

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        return user;
    }
}
