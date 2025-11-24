using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities;

public class Group
{
    public int Id { get; set; }
    public required string Description { get; set; }

    public int SystemId { get; set; }
    [JsonIgnore]
    public SystemEntity System { get; set; }
    [JsonIgnore]
    public ICollection<GroupUser> GroupUsers { get; set; }
    [JsonIgnore]
    public ICollection<PermissionGroup> PermissionGroups { get; set; }
}

