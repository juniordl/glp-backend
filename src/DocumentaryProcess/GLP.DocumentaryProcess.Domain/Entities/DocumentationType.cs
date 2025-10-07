using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("DocumentationTypes")]
public class DocumentationType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IReadOnlyCollection<Documentation> Documentations { get; set; }
}