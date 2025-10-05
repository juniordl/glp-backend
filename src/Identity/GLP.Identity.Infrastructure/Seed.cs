using GLP.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GLP.Identity.Infrastructure;

public static class Seed
{
    public static async Task InMemoryAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var um = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var rm = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        // 1) Crear roles base
        var adminRole = "Admin";
        var tesoreroRole = "Tesorero";
        if (!await rm.RoleExistsAsync(adminRole)) await rm.CreateAsync(new ApplicationRole(adminRole));
        if (!await rm.RoleExistsAsync(tesoreroRole)) await rm.CreateAsync(new ApplicationRole(tesoreroRole));

        // 2) Agregar permisos (claims) al rol Admin
        //    Tip: usa un namespace consistente para permisos
        var adminPerms = new[]
        {
            "Documentos.Read",
            "Documentos.Write",
            "Tesoreria.Pagos.Read",
            "Tesoreria.Pagos.Aprobar"
        };
        var adminRoleEntity = await rm.FindByNameAsync(adminRole);
        var existingRoleClaims = await rm.GetClaimsAsync(adminRoleEntity!);
        foreach (var p in adminPerms)
        {
            if (!existingRoleClaims.Any(c => c.Type == "perm" && c.Value == p))
                await rm.AddClaimAsync(adminRoleEntity!, new System.Security.Claims.Claim("perm", p));
        }

        // 3) Usuario admin con rol Admin
        var adminUserName = "admin";
        var admin = await um.FindByNameAsync(adminUserName);
        if (admin is null)
        {
            admin = ApplicationUser.Create(adminUserName, "Administrador");
            await um.CreateAsync(admin, "Adm1n$ecure!"); // cambia en prod
            await um.AddToRoleAsync(admin, adminRole);

            // (Opcional) permisos adicionales específicos del usuario
            await um.AddClaimAsync(admin, new System.Security.Claims.Claim("perm", "FeatureFlags.Toggle"));
        }

        // 4) Usuario operativo con rol Tesorero y permisos puntuales
        var opUserName = "tesorero1";
        var tes = await um.FindByNameAsync(opUserName);
        if (tes is null)
        {
            tes = ApplicationUser.Create(opUserName, "Operador Tesorería");
            await um.CreateAsync(tes, "Tes0!@#123");
            await um.AddToRoleAsync(tes, tesoreroRole);

            // Permisos de usuario (si no quieres que el rol los traiga)
            await um.AddClaimAsync(tes, new System.Security.Claims.Claim("perm", "Tesoreria.Pagos.Read"));
        }
    }
}