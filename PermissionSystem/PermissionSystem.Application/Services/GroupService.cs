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
    public class GroupService
    {
        private readonly PermissionSystemContext _context;

        public GroupService(PermissionSystemContext context)
        {
            _context = context;
        }

        public async Task<List<GroupDTO>> GetAllAsync()
        {
            return await _context.Groups
                .Select(x => new GroupDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    SystemId = x.SystemId
                })
                .ToListAsync();
        }

        public async Task<GroupDTO?> GetByIdAsync(int id)
        {
            return await _context.Groups
                .Where(x => x.Id == id)
                .Select(x => new GroupDTO
                {
                    Id = x.Id,
                    Description = x.Description,
                    SystemId = x.SystemId
                }).FirstOrDefaultAsync();
        }

        public async Task<GroupDTO> CreateAsync(GroupDTO dto)
        {
            var group = new Group
            {
                Description = dto.Description,
                SystemId = dto.SystemId,
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupDTO
            {
                Id = group.Id,
                Description = group.Description,
                SystemId = group.SystemId
            };
        }

        public async Task<bool> UpdateAsync(GroupDTO dto)
        {
            var group = await _context.Groups.FindAsync(dto.Id);

            if (group == null)
                return false;

            group.Description = dto.Description;
            group.SystemId = dto.SystemId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
                return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
