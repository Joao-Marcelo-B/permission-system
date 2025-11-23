using System.ComponentModel.DataAnnotations;

namespace PermissionSystem.Application.DTOs;

public class UserUpdateDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    public string? Password { get; set; }
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string? ConfirmPassword { get; set; }

    public int? SystemId { get; set; }

    public List<int>? GroupIds { get; set; }
}
