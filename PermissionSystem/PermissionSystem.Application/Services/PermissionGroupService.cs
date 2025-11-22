using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Services;

public class PermissionGroupService
{
    private readonly PermissionSystemContext _context;

    public PermissionGroupService(PermissionSystemContext context)
    {
        _context = context;
    }

    public async Task<List<PermissionGroupDTO>> GetAllAsync()
    {
        return await _context.PermissionGroups
            .Select(x => new PermissionGroupDTO
            {
                Id = x.Id,
                GroupId = x.GroupId,
                PermissionId = x.PermissionId,
            })
            .ToListAsync();
    }

    public async Task<PermissionGroupDTO?> GetByIdAsync(int id)
    {
        return await _context.PermissionGroups
            .Where(x => x.Id == id)
            .Select(x => new PermissionGroupDTO
            {
                Id = x.Id,
               GroupId= x.GroupId,
               PermissionId = x.PermissionId,
            }).FirstOrDefaultAsync();
    }

    public async Task<PermissionGroupDTO> CreateAsync(PermissionGroupDTO dto)
    {
        var permissionGroup = new PermissionGroup
        {
            GroupId = dto.GroupId,
            PermissionId = dto.PermissionId,
        };

        _context.PermissionGroups.Add(permissionGroup);
        await _context.SaveChangesAsync();

        return new PermissionGroupDTO
        {
            Id = permissionGroup.Id,
            GroupId = permissionGroup.GroupId,
            PermissionId = permissionGroup.PermissionId
        };
    }

    public async Task<bool> UpdateAsync(PermissionGroupDTO dto)
    {
        var permissionGroup = await _context.PermissionGroups.FindAsync(dto.Id);

        if (permissionGroup == null)
            return false;

        permissionGroup.GroupId = dto.GroupId;
        permissionGroup.PermissionId = dto.PermissionId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var permissionGroup = await _context.PermissionGroups.FindAsync(id);
        if (permissionGroup == null)
            return false;

        _context.PermissionGroups.Remove(permissionGroup);
        await _context.SaveChangesAsync();

        return true;
    }
}
