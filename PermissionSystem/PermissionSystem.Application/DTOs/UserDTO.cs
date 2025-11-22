using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PermissionSystem.Application.DTOs;

public class UserDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string? ConfirmPassword { get; set; }
    [Required]
    public SystemDTO? System { get; set; }
    [Required]
    public List<GroupDTO> Groups { get; set; } = new();
}
