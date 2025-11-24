namespace PermissionSystem.Application.DTOs;

public class LoginResultDTO
{
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}
