namespace GLP.DocumentaryProcess.Api.Models;

public class CreateMemberResponse
{
    public string FullName { get; set; }
    public string DocumentNumber { get; set; }
    public Guid MemberTypeId { get; set; }
}