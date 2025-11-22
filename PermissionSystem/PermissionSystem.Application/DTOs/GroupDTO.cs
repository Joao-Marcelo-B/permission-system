using PermissionSystem.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionSystem.Application.DTOs
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public int SystemId { get; set; }
    }
}
