using System.Security.Claims;
using GLP.Identity.Application;
using GLP.Identity.Domain;
using GLP.Identity.Infrastructure.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;

namespace GLP.Identity.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/auth")
            .WithTags("Identity");

        g.MapPost("/register", async ([FromBody] RegisterRequest req, UserManager<ApplicationUser> um) =>
        {
            var user = ApplicationUser.Create(req.UserName, req.FullName);
            var result = await um.CreateAsync(user, req.Password);
            return result.Succeeded ? Results.Ok() : Results.BadRequest(result.Errors);
        });
        
        g.MapPost("/login", async (
            [FromBody] LoginRequest req,
            SignInManager<ApplicationUser> sm,
            UserManager<ApplicationUser> um,
            ITokenService tokens,
            IConfiguration cfg) =>
        {
            var user = await um.FindByNameAsync(req.UserName);
            if (user is null) return Results.Unauthorized();

            var result = await sm.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: true);
            if (!result.Succeeded) return Results.Unauthorized();

            var accessToken = await tokens.CreateAccessTokenAsync(user);

            var expiresSeconds = int.Parse(cfg["Jwt:AccessTokenMinutes"] ?? "60") * 60;
            return Results.Ok(new { token_type = "Bearer", access_token = accessToken, expires_in = expiresSeconds });
        });

        g.MapPost("/logout", async (SignInManager<ApplicationUser> sm) =>
        {
            await sm.SignOutAsync();
            return Results.Ok();
        });

        g.MapGet("/me", async (UserManager<ApplicationUser> um, HttpContext http) =>
        {
            if (!(http.User.Identity?.IsAuthenticated ?? false)) return Results.Unauthorized();

            var userId = http.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null) return Results.Unauthorized();

            var user = await um.FindByIdAsync(userId);
            if (user is null) return Results.Unauthorized();

            var roles = await um.GetRolesAsync(user);
            var me = new MeResponse(user.Id, user.UserName!, user.FullName, user.EmailConfirmed, roles);
            return Results.Ok(me);
        });

        return app;
    }
}