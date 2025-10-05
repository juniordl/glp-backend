using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GLP.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GLP.Identity.Infrastructure.Token;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _cfg;
    private readonly UserManager<ApplicationUser> _um;
    private readonly RoleManager<ApplicationRole> _rm;

    public TokenService(IConfiguration cfg, UserManager<ApplicationUser> um, RoleManager<ApplicationRole> rm)
    { _cfg = cfg; _um = um; _rm = rm; }

    public async Task<string> CreateAccessTokenAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _um.GetRolesAsync(user);
        var userClaims = await _um.GetClaimsAsync(user);

        // claims base
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
        };

        // roles (Role claims)
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        // claims del usuario
        claims.AddRange(userClaims);

        // claims de cada rol (permiso heredado por rol)
        foreach (var roleName in roles)
        {
            var role = await _rm.FindByNameAsync(roleName);
            if (role != null)
            {
                var rc = await _rm.GetClaimsAsync(role);
                // evita duplicados simples
                foreach (var c in rc)
                    if (!claims.Any(x => x.Type == c.Type && x.Value == c.Value))
                        claims.Add(c);
            }
        }

        var token = new JwtSecurityToken(
            issuer: _cfg["Jwt:Issuer"],
            audience: _cfg["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:AccessTokenMinutes"] ?? "60")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}