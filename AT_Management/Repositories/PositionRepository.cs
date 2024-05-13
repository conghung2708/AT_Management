using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AT_Management.Repositories
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        private readonly ATDbContext _aTDbContext;

        public PositionRepository(ATDbContext aTDbContext) : base(aTDbContext) 
        {
            _aTDbContext = aTDbContext;
        }
        public async Task UpdateAsync(Position position)
        {
            var positionFromDb = await _aTDbContext.Position.FirstOrDefaultAsync(u => u.Id == position.Id);
            if (positionFromDb != null)
            {
                positionFromDb.Name = position.Name;
                positionFromDb.BasicSalary = position.BasicSalary;

                await _aTDbContext.SaveChangesAsync();
            }
        }
    }
}
