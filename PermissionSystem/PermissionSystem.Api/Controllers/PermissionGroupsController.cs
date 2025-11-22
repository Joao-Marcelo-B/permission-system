using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionGroupsController : Controller
{
    private readonly PermissionGroupService _service;

    public PermissionGroupsController(PermissionGroupService service)
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
            return NotFound("Vinculo de Permissão e grupos não encontrado.");

        return Ok(permission);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PermissionGroupDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(PermissionGroupDTO dto)
    {
        var updated = await _service.UpdateAsync(dto);
        if (!updated)
            return NotFound("Vinculo de Permissão e grupos não encontrado.");

        return Ok("Vinculo atualizado!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound("Vinculo de Permissão e grupos não encontrado.");

        return NoContent();
    }
}
