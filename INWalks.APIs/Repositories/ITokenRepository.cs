using Microsoft.AspNetCore.Identity;

namespace INWalks.APIs.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<String> roles);
    }
}
