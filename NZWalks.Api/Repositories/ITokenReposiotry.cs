using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repositories
{
    public interface ITokenReposiotry
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles);
    }
}
