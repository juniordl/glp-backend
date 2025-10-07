namespace GLP.DocumentaryProcess.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public string UserCreation { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string UserModification { get; set; }
    public DateTime ModificationDate { get; set; }
}