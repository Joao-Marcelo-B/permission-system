using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.Data.Entities;
using PermissionSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSystem.Application.Services
{
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
    }
}
