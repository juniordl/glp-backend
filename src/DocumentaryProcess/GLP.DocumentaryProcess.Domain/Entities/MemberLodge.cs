using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("Member_Lodges")]
public class MemberLodge: BaseEntity
{
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public Guid LodgeId { get; set; }
    public Lodge Lodge { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
