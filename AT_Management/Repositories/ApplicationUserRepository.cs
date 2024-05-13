using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AT_Management.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ATDbContext _db;
        public ApplicationUserRepository(ATDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ApplicationUser?> UpdateUserAsync(string id, ApplicationUser user)
        {
            var applicationUserFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (applicationUserFromDb == null)
            {
                return null;
            }

            applicationUserFromDb.FullName = user.FullName;
            applicationUserFromDb.PhoneNumber = user.PhoneNumber;
            applicationUserFromDb.PosId = user.PosId;

            var position = _db.Position.FirstOrDefault(u => u.Id == user.PosId);
            applicationUserFromDb.Salary = position.BasicSalary;

            await _db.SaveChangesAsync();
            return applicationUserFromDb;
        }

    }
}
