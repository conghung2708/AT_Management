using Microsoft.AspNetCore.Identity;

namespace AT_Management.Repositories.IRepositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
