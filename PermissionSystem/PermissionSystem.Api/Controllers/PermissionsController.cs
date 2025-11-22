using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly PermissionService _service;

    public PermissionsController(PermissionService service)
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
            return NotFound("Permissão não encontrada.");

        return Ok(permission);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PermissionDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut]
    public async Task<IActionResult> Update(PermissionDTO dto)
    {
        var updated = await _service.UpdateAsync(dto);
        if (!updated)
            return NotFound("Permissão não encontrada.");

        return Ok("Permissão atualizada!");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound("Permissão não encontrada.");

        return NoContent();
    }
}
