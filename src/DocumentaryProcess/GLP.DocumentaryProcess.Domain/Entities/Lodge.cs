using GLP.DocumentaryProcess.Domain.Abstractions;

namespace GLP.DocumentaryProcess.Domain.Entities;

[DbTable("Lodges")]
public class Lodge : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Contact { get; set; }
    public ICollection<MemberLodge> MemberLodges { get; set; }
}