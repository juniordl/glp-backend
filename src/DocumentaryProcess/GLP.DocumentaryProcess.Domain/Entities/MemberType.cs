using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("MemberTypes")]
public class MemberType: BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IReadOnlyCollection<Member> Members { get; set; }
}