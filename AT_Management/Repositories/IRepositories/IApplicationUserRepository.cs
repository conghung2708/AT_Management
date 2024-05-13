using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> UpdateUserAsync(string id, ApplicationUser user);
    }
}
