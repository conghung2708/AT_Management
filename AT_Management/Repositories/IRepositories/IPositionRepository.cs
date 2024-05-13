using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        void Update(Position position);
    }
}
