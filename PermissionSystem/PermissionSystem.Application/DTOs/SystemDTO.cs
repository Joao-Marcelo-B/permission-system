using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace PermissionSystem.Application.DTOs
{
    public class SystemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição do sistema é obrigatória.")]
        public string Description { get; set; } = string.Empty;
    }
}
