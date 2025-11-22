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

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userServices.GetAllUsers();
        if (users == null || users.Count() <= 0)
            return NotFound();

        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> PostCreateUser([FromBody] UserDTO userDto)
    {
        var user = await _userServices.CreateUser(userDto);
        if (user == null)
            return BadRequest(new { Message = "Não foi possível criar um novo usuário." });

        return Created("", null);
    }
}
