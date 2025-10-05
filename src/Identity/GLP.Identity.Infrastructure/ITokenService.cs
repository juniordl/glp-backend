using GLP.Identity.Domain;

namespace GLP.Identity.Infrastructure;

public interface ITokenService
{
    Task<string> CreateAccessTokenAsync(ApplicationUser user);
}
