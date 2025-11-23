using System.ComponentModel.DataAnnotations;

namespace PermissionSystem.Application.DTOs;

public class UserCreateDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string? ConfirmPassword { get; set; }
    [Required]
    public int? SystemId { get; set; }
    [Required]
    public List<int>? GroupIds { get; set; }
}
