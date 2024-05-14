using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position> UpdateAsync(Guid id, Position position);
    }
}
