using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IFormRepository : IRepository<Form>
    {
        void Update(Form form);
    }
}
