namespace PermissionSystem.Application.DTOs;

public class GroupDTO
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public int SystemId { get; set; }
}
