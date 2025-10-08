namespace GLP.DocumentaryProcess.Api;

public class CreateMemberRequest
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    public Guid MemberTypeId { get; set; }
}