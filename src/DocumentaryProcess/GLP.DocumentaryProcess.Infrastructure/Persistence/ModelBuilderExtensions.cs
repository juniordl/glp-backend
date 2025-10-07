using System.Reflection;
using GLP.DocumentaryProcess.Domain.Abstractions;
using GLP.DocumentaryProcess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GLP_DocumentaryProcess.Infrastructure.Persistence;

public static class ModelBuilderExtensions
{
    public static void ApplyConventionsForSchema(
        this ModelBuilder modelBuilder,
        string defaultSchema)
    {
        modelBuilder.HasDefaultSchema(defaultSchema);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clr = entityType.ClrType;
            if (clr == null) continue;
           
            if (typeof(BaseEntity).IsAssignableFrom(clr))
            {
                var idProp = entityType.FindProperty(nameof(BaseEntity.Id));
                if (idProp is not null)
                {
                    entityType.SetPrimaryKey(idProp);
                    idProp.SetColumnType("uniqueidentifier");
                    idProp.ValueGenerated = ValueGenerated.OnAdd;
                    idProp.SetDefaultValueSql("NEWSEQUENTIALID()");
                }
            }
            
            var dbTableAttr = clr.GetCustomAttribute<DbTableAttribute>();
            if (dbTableAttr is null) continue;
            entityType.SetTableName(dbTableAttr.TableName);
            entityType.SetSchema(defaultSchema);
        }
    }
}