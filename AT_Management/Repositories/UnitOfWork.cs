using AT_Management.Data;
using AT_Management.Repositories.IRepositories;

namespace AT_Management.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATDbContext _aTDbContext;
        private readonly IConfiguration _configuration;

        // Define a property to hold the TokenRepository
        public ITokenRepository TokenRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public IFormRepository FormRepository { get; private set; }
        // Modify the constructor to accept both ATDbContext and IConfiguration
        public UnitOfWork(ATDbContext aTDbContext, IConfiguration configuration)
        {
            _aTDbContext = aTDbContext;
            _configuration = configuration;

            // Initialize the TokenRepository using IConfiguration
            TokenRepository = new TokenRepository(_configuration);
            ApplicationUserRepository = new ApplicationUserRepository(_aTDbContext);
            PositionRepository = new PositionRepository(_aTDbContext);
            FormRepository = new FormRepository(_aTDbContext);
        }

        public async Task SaveAsync()
        {
            await _aTDbContext.SaveChangesAsync();
        }

    }
}