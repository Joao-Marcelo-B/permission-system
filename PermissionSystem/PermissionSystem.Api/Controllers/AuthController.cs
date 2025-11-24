using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
        var result = await _authServices.LoginAsync(dto);
        if (result == null)
            return Unauthorized(new { Message = "Email ou senha inválidos"} );

        return Ok(result);
    }
}
