namespace GLP.DocumentaryProcess.Api;

public class UpdateMemberRequest
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    
    public string DocumentNumber { get; set; }
    public Guid MemberTypeId { get; set; }
}