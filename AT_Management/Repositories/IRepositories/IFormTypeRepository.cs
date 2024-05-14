using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IFormTypeRepository : IRepository<FormType>
    {
        Task Update(FormType formType);
    }
}
