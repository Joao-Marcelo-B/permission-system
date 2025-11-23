using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionSystem.Application.Data.Entities;

[Table("System")]
public class SystemEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Description { get; set; }

    public ICollection<User>? Users { get; set; }
    public ICollection<Group>? Groups { get; set; }
}
