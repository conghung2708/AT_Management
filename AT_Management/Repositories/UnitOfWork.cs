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

        // Modify the constructor to accept both ATDbContext and IConfiguration
        public UnitOfWork(ATDbContext aTDbContext, IConfiguration configuration)
        {
            _aTDbContext = aTDbContext;
            _configuration = configuration;

            // Initialize the TokenRepository using IConfiguration
            TokenRepository = new TokenRepository(_configuration);
        }

       
    }
}