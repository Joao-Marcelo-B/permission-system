using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Services;

public class PermissionService
{
    private readonly PermissionSystemContext _context;

    public PermissionService(PermissionSystemContext context)
    {
        _context = context;
    }

    public async Task<List<PermissionDTO>> GetAllAsync()
    {
        return await _context.Permissions
            .Select(x => new PermissionDTO
            {
                Id = x.Id,
                Description = x.Description
            })
            .ToListAsync();
    }

    public async Task<PermissionDTO?> GetByIdAsync(int id)
    {
        return await _context.Permissions
            .Where(x => x.Id == id)
            .Select(x => new PermissionDTO
            {
                Id = x.Id,
                Description = x.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PermissionDTO> CreateAsync(PermissionDTO dto)
    {
        var permission = new Permission
        {
            Description = dto.Description
        };

        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();

        return new PermissionDTO
        {
            Id = permission.Id,         
            Description = permission.Description
        };
    }

    public async Task<bool> UpdateAsync(PermissionDTO dto)
    {
        var permission = await _context.Permissions.FindAsync(dto.Id);

        if (permission == null)
            return false;

        permission.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null)
            return false;

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Permission>> GetPermissionsByUserIdAsync(int userId)
    {
        var permissionsByUser = new List<Permission>();

        var groupsUser = await _context.GroupUsers
            .Where(gu => gu.UserId == userId)
            .ToListAsync();

        if (groupsUser.Count == 0)
            return permissionsByUser;

        var groups = await _context.Groups.ToListAsync();
        var validGroups = groups
            .Where(g => groupsUser.Any(gu => gu.GroupId == g.Id))
            .ToList();

        var permissions = await _context.Permissions.ToListAsync();
        var permissionGroups = await _context.PermissionGroups.ToListAsync();

        foreach (var permission in permissions)
        {
            foreach (var permissionGroup in permissionGroups)
            {
                if (permission.Id == permissionGroup.PermissionId &&
                    validGroups.Any(vg => vg.Id == permissionGroup.GroupId))
                {
                    permissionsByUser.Add(permission);
                }
            }
        }

        return permissionsByUser;
    }
}