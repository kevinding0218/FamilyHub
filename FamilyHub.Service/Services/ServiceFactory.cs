using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using FamilyHub.Repository.Contracts.Finance;
using FamilyHub.Repository.Contracts.Transactions;
using FamilyHub.Repository.Repository.Common;
using FamilyHub.Repository.Repository.Finance;
using FamilyHub.Repository.Repository.Transactions;
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

        #region Common Contracts
        protected IUserRepository _userRepository;
        #endregion

        #region Payment Contracts
        protected IPaymentPayorRepository _paymentPayorRepository;
        protected IPaymentPayorRelationshipRepository _paymentPayorRelationshipRepository;
        protected IPaymentMethodRepository _paymentMethodRepository;
        #endregion

        #region Transaction Contracts
        protected ITransactionCategoryRepository _transactionCategoryRepository;
        protected ITransactionTypeRepository _transactionTypeRepository;
        protected ITransactionDetailRepository _transactionDetailRepository;
        protected ITransactionRepository _transactionRepository;
        #endregion

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

        #region Common Instances
        protected IUserRepository UserRepository
            => _userRepository ?? (_userRepository = new UserRepository(_dbContext));
        #endregion

        #region Payment Instances
        protected IPaymentPayorRepository PaymentPayorRepository
            => _paymentPayorRepository ?? (_paymentPayorRepository = new PaymentPayorRepository(_dbContext));
        protected IPaymentPayorRelationshipRepository PaymentPayorRelationshipRepository
            => _paymentPayorRelationshipRepository ?? (_paymentPayorRelationshipRepository = new PaymentPayorRelationshipRepository(_dbContext));
        protected IPaymentMethodRepository PaymentMethodRepository
            => _paymentMethodRepository ?? (_paymentMethodRepository = new PaymentMethodRepository(_dbContext));
        #endregion

        #region Transaction Instances
        protected ITransactionCategoryRepository TransactionCategoryRepository
            => _transactionCategoryRepository ?? (_transactionCategoryRepository = new TransactionCategoryRepository(_dbContext));
        protected ITransactionTypeRepository TransactionTypeRepository
            => _transactionTypeRepository ?? (_transactionTypeRepository = new TransactionTypeRepository(_dbContext));
        protected ITransactionDetailRepository TransactionDetailRepository
            => _transactionDetailRepository ?? (_transactionDetailRepository = new TransactionDetailRepository(_dbContext));
        protected ITransactionRepository TransactionRepository
            => _transactionRepository ?? (_transactionRepository = new TransactionRepository(_dbContext));
        #endregion

    }
}
