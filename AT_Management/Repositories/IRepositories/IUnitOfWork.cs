﻿namespace AT_Management.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ITokenRepository TokenRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }
        IPositionRepository PositionRepository { get; }
        IFormRepository FormRepository { get; }
        IFormTypeRepository FormTypeRepository { get; }
        IImageRepository ImageRepository { get; }
        Task SaveAsync();
    }
}
