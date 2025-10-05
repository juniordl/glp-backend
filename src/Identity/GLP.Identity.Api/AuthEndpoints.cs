using System.Security.Claims;
using GLP.Identity.Application;
using GLP.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GLP.Identity.Api;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
    {
        var g = app.MapGroup("/auth");

        g.MapPost("/register", async ([FromBody] RegisterRequest req, UserManager<ApplicationUser> um) =>
        {
            var user = ApplicationUser.Create(req.UserName, req.FullName);
            var result = await um.CreateAsync(user, req.Password);
            return result.Succeeded ? Results.Ok() : Results.BadRequest(result.Errors);
        });

        g.MapPost("/login", async ([FromBody] LoginRequest req, SignInManager<ApplicationUser> sm, UserManager<ApplicationUser> um) =>
        {
            var user = await um.FindByNameAsync(req.UserName);
            if (user is null) return Results.Unauthorized();

            var valid = await um.CheckPasswordAsync(user, req.Password);
            if (!valid) return Results.Unauthorized();

            var result = await sm.PasswordSignInAsync(user, req.Password, req.RememberMe, lockoutOnFailure: true);
            return result.Succeeded ? Results.Ok() : Results.Unauthorized();
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