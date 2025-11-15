namespace PermissionSystem.Application.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public int SystemId { get; set; }
    public SystemEntity System { get; set; }

    public ICollection<GroupUser> GroupUsers { get; set; }
}
