namespace GLP.DocumentaryProcess.Domain.Entities;

public class Member: BaseEntity
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    
    public Guid DocumentationId { get; set; }
    public Documentation Documentation { get; set; }
    
    public Guid MemberTypeId { get; set; }
    public MemberType MemberType { get; set; }
    
    public Guid LodgeId { get; set; }
    public Lodge Lodge { get; set; }
    
}