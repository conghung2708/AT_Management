using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form> UpdateAsync(Guid id, Form form);
    }
}
