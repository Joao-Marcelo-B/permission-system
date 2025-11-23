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

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int? userId)
    {
        if (userId == null || userId <= 0)
            return NotFound();

        var user = await _userServices.GetUserById(userId.Value);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> PostCreateUser([FromBody] UserCreateDTO userCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userServices.CreateUser(userCreateDto);
        if (user == null)
            return BadRequest(new { Message = "Não foi possível criar um novo usuário." });

        return Created("", null);
    }
}
