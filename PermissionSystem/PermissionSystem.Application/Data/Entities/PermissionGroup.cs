using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities;

public class PermissionGroup
{
    public int Id { get; set; }

    public int GroupId { get; set; }
    [JsonIgnore]
    public Group Group { get; set; }

    public int PermissionId { get; set; }
    [JsonIgnore]
    public Permission Permission { get; set; }
}

