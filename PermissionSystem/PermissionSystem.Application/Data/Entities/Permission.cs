namespace PermissionSystem.Application.Data.Entities;

public class Permission
{
    public int Id { get; set; }
    public required string Description { get; set; }

    public ICollection<PermissionGroup>? PermissionGroups { get; set; }
}
