using Microsoft.EntityFrameworkCore;
using PermissionSystem.Application.Data;
using PermissionSystem.Application.DTOs;

namespace PermissionSystem.Application.Services;

public class AuthServices
{
    private readonly PermissionSystemContext _context;
    private readonly TokenService _tokenService;
    public AuthServices(PermissionSystemContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResultDTO?> LoginAsync(LoginDTO dto)
    {
        var user = await _context.Users
            .Include(u => u.GroupUsers!)
                .ThenInclude(gu => gu.Group!)
                    .ThenInclude(g => g.PermissionGroups!)
                        .ThenInclude(pg => pg.Permission)
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user == null)
            return null;

        if (!user.Password.Equals(dto.Password))
            return null;

        var permissions = user.GroupUsers!
            .SelectMany(gu => gu.Group!.PermissionGroups!)
            .Select(pg => pg.Permission!.Description)
            .Distinct()
            .ToList();

        var token = _tokenService.GenerateToken(user, permissions);

        return new LoginResultDTO
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(1)
        };
    }
}
