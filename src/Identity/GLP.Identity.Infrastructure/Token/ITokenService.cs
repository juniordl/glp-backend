using GLP.Identity.Domain;

namespace GLP.Identity.Infrastructure.Token;

public interface ITokenService
{
    Task<string> CreateAccessTokenAsync(ApplicationUser user);
}
