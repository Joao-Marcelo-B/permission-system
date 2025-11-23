namespace PermissionSystem.Application.Data.Entities;

public class Group
{
    public int Id { get; set; }
    public required string Description { get; set; }

    public int SystemId { get; set; }
    public SystemEntity? System { get; set; }

    public ICollection<GroupUser>? GroupUsers { get; set; }
    public ICollection<PermissionGroup>? PermissionGroups { get; set; }
}

