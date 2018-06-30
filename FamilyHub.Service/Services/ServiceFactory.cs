using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using FamilyHub.Repository.Contracts.Finance;
using FamilyHub.Repository.Repository.Common;
using FamilyHub.Repository.Repository.Finance;
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
        protected IPaymentPayorRepository _paymentPayorRepository;
        protected IPaymentPayorRelationshipRepository _paymentPayorRelationshipRepository;

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
        protected IPaymentPayorRepository PaymentPayorRepository
            => _paymentPayorRepository ?? (_paymentPayorRepository = new PaymentPayorRepository(_dbContext));
        protected IPaymentPayorRelationshipRepository PaymentPayorRelationshipRepository
            => _paymentPayorRelationshipRepository ?? (_paymentPayorRelationshipRepository = new PaymentPayorRelationshipRepository(_dbContext));
    }
}
