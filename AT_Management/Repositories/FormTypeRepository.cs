using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AT_Management.Repositories
{
    public class FormTypeRepository : Repository<FormType>, IFormTypeRepository
    {
        private readonly ATDbContext _aTDbContext;

        public FormTypeRepository(ATDbContext aTDbContext) : base(aTDbContext)
        {
            _aTDbContext = aTDbContext;
        }

        public async Task<FormType> Update(Guid id, FormType formType)
        {
            var formTypeFromDb = await _aTDbContext.FormType.FirstOrDefaultAsync(u => u.Id == id);

            if (formTypeFromDb == null)
            {
                return null;
            }
            formTypeFromDb.Name = formType.Name;
            await _aTDbContext.SaveChangesAsync();

            return formTypeFromDb;
        }
    }
}
