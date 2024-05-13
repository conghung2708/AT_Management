namespace AT_Management.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ITokenRepository TokenRepository { get; }
      
    }
}
