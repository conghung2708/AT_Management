using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IFormTypeRepository : IRepository<FormType>
    {
        Task<FormType> Update(Guid id, FormType formType);
    }
}
