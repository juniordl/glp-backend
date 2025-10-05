using Microsoft.AspNetCore.Identity;

namespace GLP.Identity.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; private set; }
    public bool IsActive { get; private set; } = true;

    private ApplicationUser() { }

    public static ApplicationUser Create(string userName, string? fullName = null)
        => new()
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            FullName = fullName,
            LockoutEnabled = true
        };

    public void Deactivate() => IsActive = false;
    public void UpdateFullName(string? name) => FullName = name;
}