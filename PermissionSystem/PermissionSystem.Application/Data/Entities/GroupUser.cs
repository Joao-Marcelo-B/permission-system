using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities;

public class GroupUser
{
    public int Id { get; set; }

    public int UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }

    public int GroupId { get; set; }
    [JsonIgnore]
    public Group Group { get; set; }
}
