namespace GLP.DocumentaryProcess.Domain.Entities;

public class Documentation : BaseEntity
{
    public string Path { get; set; }
    public string RelativePath { get; set; }
    public Guid DocumentationTypeId { get; set; }
    public DocumentationType DocumentationType { get; set; }
}