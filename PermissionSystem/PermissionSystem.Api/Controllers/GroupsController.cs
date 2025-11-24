using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly GroupService _service;

    public GroupsController(GroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var permission = await _service.GetByIdAsync(id);
        if (permission == null)
            return NotFound("Grupo não encontrado.");

        return Ok(permission);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GroupDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(GroupDTO dto)
    {
        var updated = await _service.UpdateAsync(dto);
        if (!updated)
            return NotFound("Grupo não encontrado.");

        return Ok("Grupo atualizado!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound("Grupo não encontrado.");

        return NoContent();
    }

    [HttpGet("groups-by-system/{id}")]
    public async Task<IActionResult> GetGroupsBySystem(int id)
    {
        var groups = await _service.GetGroupsBySystem(id);
        return Ok(groups);
    }

}
