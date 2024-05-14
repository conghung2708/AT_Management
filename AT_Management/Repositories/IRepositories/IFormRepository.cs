using AT_Management.Models.Domain;

namespace AT_Management.Repositories.IRepositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<List<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(Guid id);
        Task<Form> CreateFormAsync(Form form, IFormFile imageFile);
        Task<Form> UpdateFormAsync(Guid id, Form form);
        Task DeleteFormAsync(Guid id);
    }
}
