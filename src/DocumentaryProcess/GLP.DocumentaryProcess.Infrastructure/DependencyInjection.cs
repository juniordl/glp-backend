using GLP_DocumentaryProcess.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GLP_DocumentaryProcess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDocumentaryProcessInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<DocumentaryProcessDbContext>(opt => opt.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));

        return services;
    }
}