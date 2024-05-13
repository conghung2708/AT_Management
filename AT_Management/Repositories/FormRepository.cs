using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;

namespace AT_Management.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly ATDbContext _aTDbContext;

        public FormRepository(ATDbContext aTDbContext) : base(aTDbContext)
        {
            _aTDbContext = aTDbContext;
        }

        public void Update(Form form)
        {
            var formFromDb = _aTDbContext.Form.FirstOrDefault(u => u.FormId == form.FormId);
            if (formFromDb != null)
            {
                formFromDb.Description = form.Description;
                formFromDb.Type = form.Type;
            }
        }
    }
}
