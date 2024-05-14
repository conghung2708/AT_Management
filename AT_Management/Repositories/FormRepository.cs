using System;
using System.IO; // Add System.IO for Path class
using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace AT_Management.Repositories
{
    public class FormRepository : Repository<Form>, IFormRepository
    {
        private readonly ATDbContext _aTDbContext;
        private readonly IImageRepository _imageRepository;
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IPositionRepository _positionRepository;

        public FormRepository(ATDbContext aTDbContext, IImageRepository imageRepository/*, IApplicationUserRepository applicationUserRepository, IPositionRepository positionRepository*/) : base(aTDbContext)
        {
            _aTDbContext = aTDbContext;
            _imageRepository = imageRepository;
            //_applicationUserRepository = applicationUserRepository;
            //_positionRepository = positionRepository;
        }

        public async Task<Form> CreateFormAsync(Form form, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile), "Image file cannot be null.");
            }

            var image = new Image
            {
                Id = Guid.NewGuid(),
                File = imageFile,
                FileName = Path.GetFileNameWithoutExtension(imageFile.FileName),
                FileExtension = Path.GetExtension(imageFile.FileName),
                FileSizeInBytes = imageFile.Length
            };

            var uploadedImage = await _imageRepository.Upload(image);
            form.ImageId = uploadedImage.Id;
            form.Image = uploadedImage;

            _aTDbContext.Form.Add(form);

            // Update user salary if the form type matches the specified GUID
            if (form.FormTypeId == Guid.Parse("D9B9A5A7-8F5F-4C32-B3A6-DC1E1EAB4B6C"))
            {
                var user = await _aTDbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == form.UserId);
                if (user != null)
                {
                    var position = await _aTDbContext.Position.FirstOrDefaultAsync(p => p.Id == user.PosId);
                    if (position != null)
                    {
                        // Update the user's salary
                        user.Salary -= position.BasicSalary / 30;
                        _aTDbContext.ApplicationUsers.Update(user);
                    }
                }
            }
            await _aTDbContext.SaveChangesAsync();

            return form;
        }

        public async Task<Form> UpdateFormAsync(Guid id, Form form)
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

        public async Task<List<Form>> GetAllFormsAsync()
        {
            return await _aTDbContext.Form
                .Include(f => f.User) // Include the User entity
                .Include(f => f.FormType) // Include the FormType entity
                .Include(f => f.Image) // Include the Image entity
                .ToListAsync();
        }

        public async Task<Form> GetFormByIdAsync(Guid id)
        {
            return await _aTDbContext.Form
                .Include(f => f.User) // Include the User entity
                .Include(f => f.FormType) // Include the FormType entity
                .Include(f => f.Image) // Include the Image entity
                .FirstOrDefaultAsync(f => f.FormId == id);
        }

        public async Task DeleteFormAsync(Guid id)
        {
            var form = await _aTDbContext.Form.FindAsync(id);
            if (form != null)
            {
                _aTDbContext.Form.Remove(form);
                await _aTDbContext.SaveChangesAsync();
            }
        }
    }
}
