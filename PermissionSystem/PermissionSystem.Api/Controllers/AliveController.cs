using Microsoft.AspNetCore.Mvc;

namespace PermissionSystem.Api.Controllers;

/// <summary>
/// Controller para verificar status da API
/// </summary>
[ApiController]
[Route("[controller]")]
public class AliveController : ControllerBase
{
    /// <summary>
    /// Endpoint para verificar o status da API
    /// </summary>
    [HttpGet]
    public void GetAlive() =>
        Ok($"System alive! {DateTime.Now}");
}
