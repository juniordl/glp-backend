using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("Members")]
public class Member : BaseEntity
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    //TODO: add type document (enum)
    public ICollection<Documentation> Documentations { get; set; }
    public Guid MemberTypeId { get; set; }
    public MemberType MemberType { get; set; }
    public ICollection<MemberLodge> MemberLodges { get; set; }
}