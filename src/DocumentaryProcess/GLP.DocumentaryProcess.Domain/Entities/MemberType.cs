namespace GLP.DocumentaryProcess.Domain.Entities;

public class MemberType: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    private readonly List<Member> _members = new();
    public IReadOnlyCollection<Member> Members => _members;
    
}