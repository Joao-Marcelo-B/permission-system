using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities
{
    [Table("System")]
    public class SystemEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
        [JsonIgnore]

        public ICollection<User> Users { get; set; } = new List<User>();
        [JsonIgnore]
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
