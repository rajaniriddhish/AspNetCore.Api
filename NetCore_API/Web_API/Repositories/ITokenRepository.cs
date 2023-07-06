using Microsoft.AspNetCore.Identity;

namespace Web_API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
