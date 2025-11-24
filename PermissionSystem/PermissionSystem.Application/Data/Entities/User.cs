using System.Text.Json.Serialization;

namespace PermissionSystem.Application.Data.Entities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public int SystemId { get; set; }
    [JsonIgnore]
    public SystemEntity? System { get; set; }
    [JsonIgnore]
    public ICollection<GroupUser>? GroupUsers { get; set; }
}
