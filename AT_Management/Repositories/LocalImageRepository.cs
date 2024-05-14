using AT_Management.Data;
using AT_Management.Models.Domain;
using AT_Management.Repositories.IRepositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AT_Management.Repositories
{
    public class LocalImageRepository : IImageRepository // Implement an interface if needed
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ATDbContext _aTDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, ATDbContext aTDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _aTDbContext = aTDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
               $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            // Add Image to the Images table
            await _aTDbContext.Image.AddAsync(image);
            await _aTDbContext.SaveChangesAsync();
            return image;
        }
    }
}
