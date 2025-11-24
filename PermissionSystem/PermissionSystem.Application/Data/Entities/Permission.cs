using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Description { get; set; }
    [JsonIgnore]

    public ICollection<PermissionGroup> PermissionGroups { get; set; }
}
