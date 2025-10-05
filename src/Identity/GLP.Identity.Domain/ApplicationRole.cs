using Microsoft.AspNetCore.Identity;

namespace GLP.Identity.Domain;

public sealed class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() { }
    public ApplicationRole(string name) : base(name) { Id = Guid.NewGuid(); }
}