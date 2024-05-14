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

        public async Task Update(FormType formType)
        {
            var formTypeFromDb = await _aTDbContext.FormType.FirstOrDefaultAsync(u => u.Id == formType.Id);
            if (formTypeFromDb != null)
            {
                formTypeFromDb.Name = formTypeFromDb.Name;
                await _aTDbContext.SaveChangesAsync();
            }
        }
    }
}
