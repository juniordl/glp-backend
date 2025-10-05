using GLP.Identity.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GLP.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        // EF Core InMemory para prototipo/tests
        services.AddDbContext<AuthDbContext>(opt => opt.UseInMemoryDatabase("AuthDb"));

        services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.User.RequireUniqueEmail = false;                 // sin email
                opt.SignIn.RequireConfirmedAccount = false;          // sin confirmación
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        // Cookies (puedes cambiar a JWT después)
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.Cookie.Name = "__Host.GLP";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                opt.Cookie.SameSite = SameSiteMode.Lax; // None si cruzas dominios
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromHours(8);
            });

        return services;
    }
}