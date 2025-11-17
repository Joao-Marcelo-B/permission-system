namespace PermissionSystem.Application.Data.Entities;

public class SystemEntity
{
    public int Id { get; set; }
    public string Description { get; set; }

   
    public ICollection<User> Users { get; set; }
    public ICollection<Group> Groups { get; set; }
}