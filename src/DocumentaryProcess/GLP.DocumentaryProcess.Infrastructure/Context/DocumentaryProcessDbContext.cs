using GLP_DocumentaryProcess.Infrastructure.Persistence;
using GLP.DocumentaryProcess.Domain;
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
    public DbSet<MemberLodge> MemberLodges => Set<MemberLodge>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Member>()
            .HasOne(m => m.MemberType)
            .WithMany(m => m.Members)
            .HasForeignKey(m => m.MemberTypeId);

        modelBuilder.Entity<Documentation>()
            .HasOne(d => d.Member)
            .WithMany(m => m.Documentations)
            .HasForeignKey(d => d.MemberId);
        
        modelBuilder.Entity<Documentation>()
            .HasOne(d => d.DocumentationType)
            .WithMany(m => m.Documentations)
            .HasForeignKey(d => d.DocumentationTypeId);
        
        modelBuilder.Entity<MemberLodge>()
            .HasKey(ml => new { ml.MemberId, ml.LodgeId });

        modelBuilder.Entity<MemberLodge>()
            .HasOne(ml => ml.Member)
            .WithMany(m => m.MemberLodges)
            .HasForeignKey(ml => ml.MemberId);

        modelBuilder.Entity<MemberLodge>()
            .HasOne(ml => ml.Lodge)
            .WithMany(l => l.MemberLodges)
            .HasForeignKey(ml => ml.LodgeId);
        
        modelBuilder.ApplyConventionsForSchema("doc");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DocumentaryProcessDbContext).Assembly);
    }
}