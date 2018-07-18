using AutoMapper;
using FamilyHub.Data;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Common;
using FamilyHub.Repository.Contracts.Member;
using FamilyHub.Repository.Contracts.Payment;
using FamilyHub.Repository.Contracts.Transactions;
using FamilyHub.Repository.Repository.Common;
using FamilyHub.Repository.Repository.Member;
using FamilyHub.Repository.Repository.Payment;
using FamilyHub.Repository.Repository.Transactions;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Services
{
    public abstract class ServiceFactory : IServiceFactory
    {
        protected bool Disposed;
        #region Auto Mapper
        public readonly IMapper _mapper;
        #endregion

        private readonly IUserInfo _userInfo;
        protected readonly FamilyHubDbContext _dbContext;

        #region Common Contracts
        protected IUserRepository _userRepository;
        #endregion

        #region Member Contacts
        protected IMemberRelationshipRepository _memberRelationshipRepository;
        protected IMemberContactRepository _memberContactRepository;
        #endregion

        #region Payment Contracts
        protected IPaymentPayorRepository _paymentPayorRepository;
        protected IPaymentMethodTypeRepository _paymentMethodTypeRepository;
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

        public ServiceFactory(IMapper mapper, FamilyHubDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public ServiceFactory(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
        {
            _mapper = mapper;
            _userInfo = userInfo;
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
            => _userRepository ?? (_userRepository = new UserRepository(_userInfo, _dbContext));
        #endregion

        #region Member Instances
        protected IMemberRelationshipRepository MemberRelationshipRepository
            => _memberRelationshipRepository ?? (_memberRelationshipRepository = new MemberRelationshipRepository(_dbContext));
        protected IMemberContactRepository MemberContactRepository
            => _memberContactRepository ?? (_memberContactRepository = new MemberContactRepository(_userInfo, _dbContext));
        #endregion

        #region Payment Instances
        protected IPaymentPayorRepository PaymentPayorRepository
            => _paymentPayorRepository ?? (_paymentPayorRepository = new PaymentPayorRepository(_userInfo, _dbContext));
        protected IPaymentMethodTypeRepository PaymentMethodTypeRepository
            => _paymentMethodTypeRepository ?? (_paymentMethodTypeRepository = new PaymentMethodTypeRepository(_dbContext));
        protected IPaymentMethodRepository PaymentMethodRepository
            => _paymentMethodRepository ?? (_paymentMethodRepository = new PaymentMethodRepository(_userInfo, _dbContext));
        #endregion

        #region Transaction Instances
        protected ITransactionCategoryRepository TransactionCategoryRepository
            => _transactionCategoryRepository ?? (_transactionCategoryRepository = new TransactionCategoryRepository(_userInfo, _dbContext));
        protected ITransactionTypeRepository TransactionTypeRepository
            => _transactionTypeRepository ?? (_transactionTypeRepository = new TransactionTypeRepository(_userInfo, _dbContext));
        protected ITransactionDetailRepository TransactionDetailRepository
            => _transactionDetailRepository ?? (_transactionDetailRepository = new TransactionDetailRepository(_userInfo, _dbContext));
        protected ITransactionRepository TransactionRepository
            => _transactionRepository ?? (_transactionRepository = new TransactionRepository(_userInfo, _dbContext));
        #endregion

    }
}
