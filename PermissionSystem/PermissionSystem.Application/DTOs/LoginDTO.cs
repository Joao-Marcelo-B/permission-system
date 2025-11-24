using System.ComponentModel.DataAnnotations;

namespace PermissionSystem.Application.DTOs;

public class LoginDTO
{
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}
