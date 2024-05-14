using AT_Management.Data;
using AT_Management.Repositories.IRepositories;
using Microsoft.AspNetCore.Hosting;

namespace AT_Management.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATDbContext _aTDbContext;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageRepository _imageRepository;

        // Define a property to hold the TokenRepository
        public ITokenRepository TokenRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public IFormRepository FormRepository { get; private set; }
        public IFormTypeRepository FormTypeRepository { get; private set; }
        public IImageRepository ImageRepository { get; private set; }

        // Modify the constructor to accept both ATDbContext and IConfiguration
        public UnitOfWork(ATDbContext aTDbContext, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IImageRepository imageRepository)
        {
            _aTDbContext = aTDbContext;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _imageRepository = imageRepository;

            // Initialize the TokenRepository using IConfiguration
            TokenRepository = new TokenRepository(_configuration);
            ApplicationUserRepository = new ApplicationUserRepository(_aTDbContext);
            PositionRepository = new PositionRepository(_aTDbContext);
            FormRepository = new FormRepository(_aTDbContext, _imageRepository);
            FormTypeRepository = new FormTypeRepository(_aTDbContext);
            ImageRepository = new LocalImageRepository(_webHostEnvironment, _httpContextAccessor, _aTDbContext);
        }

        public async Task SaveAsync()
        {
            await _aTDbContext.SaveChangesAsync();
        }
    }
}