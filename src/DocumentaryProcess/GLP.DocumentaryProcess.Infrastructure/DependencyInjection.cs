using GLP_DocumentaryProcess.Infrastructure.Context;
using GLP_DocumentaryProcess.Infrastructure.Persistence.Repositories;
using GLP_DocumentaryProcess.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GLP_DocumentaryProcess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDocumentaryProcessInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
        services.AddDbContext<DocumentaryProcessDbContext>(opt => opt.UseSqlServer(cfg.GetConnectionString("DefaultConnection")));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        return services;
    }
}