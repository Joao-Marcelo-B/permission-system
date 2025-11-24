using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Services;

public class SystemService
{
    private readonly PermissionSystemContext _context;

    public SystemService(PermissionSystemContext context)
    {
        _context = context;
    }

    public async Task<List<SystemDTO>> GetAllAsync()
    {
        return await _context.Systems
            .Select(x => new SystemDTO
            {
                Id = x.Id,
                Description = x.Description
            })
            .ToListAsync();
    }

    public async Task<SystemDTO?> GetByIdAsync(int id)
    {
        return await _context.Systems
            .Where(x => x.Id == id)
            .Select(x => new SystemDTO
            {
                Id = x.Id,
                Description = x.Description
            })
            .FirstOrDefaultAsync();
    }

    public async Task<SystemDTO> CreateAsync(SystemDTO dto)
    {
        var system = new SystemEntity
        {
            Description = dto.Description
        };

        _context.Systems.Add(system);
        await _context.SaveChangesAsync();

        return new SystemDTO
        {
            Id = system.Id,
            Description = system.Description
        };
    }

    public async Task<bool> UpdateAsync(SystemDTO dto)
    {
        var system = await _context.Systems.FindAsync(dto.Id);

        if (system == null)
            return false;

        system.Description = dto.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var system = await _context.Systems.FindAsync(id);
        if (system == null)
            return false;

        _context.Systems.Remove(system);
        await _context.SaveChangesAsync();

        return true;
    }
}
