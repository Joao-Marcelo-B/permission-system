namespace PermissionSystem.Application.DTOs;

public class UserDTO
{
    public string Name { get; set; } = string.Empty;
    public required string Email { get; set; }
    public SystemDTO? System { get; set; }
    public required ICollection<GroupDTO> Groups { get; set; }
}
