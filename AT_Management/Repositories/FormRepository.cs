using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AT_Management.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly ATDbContext _aTDbContext;

        public FormRepository(ATDbContext aTDbContext) : base(aTDbContext)
        {
            _aTDbContext = aTDbContext;
        }



        public async Task<Form> UpdateAsync(Guid id, Form form)
        {
            var formFromDb = await _aTDbContext.Form.FirstOrDefaultAsync(u => u.FormId == id);
            if (formFromDb == null)
            {
                return null;
            }

            formFromDb.Description = form.Description;
            formFromDb.FormTypeId = form.FormTypeId;

            await _aTDbContext.SaveChangesAsync();
            return formFromDb;
        }
    }
}
