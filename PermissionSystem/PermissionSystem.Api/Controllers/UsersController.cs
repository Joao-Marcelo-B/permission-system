using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersServices _userServices;

    public UsersController(UsersServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<IActionResult> PostCreateUser([FromBody] UserDTO user)
    {
        var result = await _userServices.CreateUser(user);

        return Created("", null);
    }
}
