using Microsoft.AspNetCore.Mvc;
using PermissionSystem.Application.DTOs;
using PermissionSystem.Application.Services;

namespace PermissionSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemController : ControllerBase
{
    private readonly SystemService _service;

    public SystemController(SystemService service)
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
        var system = await _service.GetByIdAsync(id);
        if (system == null)
            return NotFound("Sistema não encontrado.");

        return Ok(system);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SystemDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SystemDTO dto)
    {
        dto.Id = id; // garantir que o id do DTO será o do caminho

        var updated = await _service.UpdateAsync(dto);
        if (!updated)
            return NotFound("Sistema não encontrado.");

        return Ok("Sistema atualizado!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound("Sistema não encontrado.");

        return NoContent();
    }
}
