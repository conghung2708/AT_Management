using AT_Management.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace AT_Management.Repositories.IRepositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
