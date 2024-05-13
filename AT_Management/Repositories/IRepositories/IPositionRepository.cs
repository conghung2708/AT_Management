using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task UpdateAsync(Position position);
    }
}
