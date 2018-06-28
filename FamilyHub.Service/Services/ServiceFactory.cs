using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using FamilyHub.Repository.Repository.Common;
using FamilyHub.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Services
{
    public abstract class ServiceFactory : IServiceFactory
    {
        protected bool Disposed;
        protected readonly FamilyHubDbContext _dbContext;

        protected IUserRepository _userRepository;

        public ServiceFactory(FamilyHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                _dbContext?.Dispose();

                Disposed = true;
            }
        }

        protected IUserRepository UserRepository
            => _userRepository ?? (_userRepository = new UserRepository(_dbContext));
    }
}
