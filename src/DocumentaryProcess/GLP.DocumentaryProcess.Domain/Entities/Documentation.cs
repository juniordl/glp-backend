using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("Documentations")]
public class Documentation : BaseEntity
{
    public string Path { get; set; }
    public string RelativePath { get; set; }
    public Guid DocumentationTypeId { get; set; }
    public DocumentationType DocumentationType { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
}