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

        var role = "Admin";
        if (!await rm.RoleExistsAsync(role))
            await rm.CreateAsync(new ApplicationRole(role));

        var user = ApplicationUser.Create("admin", "Administrador");
        var exists = await um.FindByNameAsync(user.UserName!);
        if (exists is null)
        {
            await um.CreateAsync(user, "Adm1n$ecure!");
            await um.AddToRoleAsync(user, role);
        }
    }
}