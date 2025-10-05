using GLP.DocumentaryProcess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GLP_DocumentaryProcess.Infrastructure.Context;

public class DocumentaryProcessDbContext : DbContext
{
    public DocumentaryProcessDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Member> Members => Set<Member>();
    public DbSet<MemberType> MemberTypes => Set<MemberType>();
    public DbSet<Lodge> Lodges => Set<Lodge>();
    public DbSet<Documentation> Documentations => Set<Documentation>();
    public DbSet<DocumentationType> DocumentationTypes => Set<DocumentationType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("doc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentaryProcessDbContext).Assembly);
    }
}