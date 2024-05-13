using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;

namespace AT_Management.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ATDbContext _db;
        public ApplicationUserRepository(ATDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
