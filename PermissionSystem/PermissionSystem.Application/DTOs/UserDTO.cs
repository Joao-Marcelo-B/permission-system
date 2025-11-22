using System.ComponentModel.DataAnnotations;

namespace PermissionSystem.Application.DTOs;

public class UserDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; } = string.Empty;
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public required string ConfirmPassword { get; set; }
    [Required]
    public int SystemId { get; set; }
}
