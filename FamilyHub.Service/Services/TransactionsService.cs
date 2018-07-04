using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class TransactionsService : ServiceFactory, ITransactionsService
    {
        public TransactionsService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public TransactionsService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public TransactionsService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        public async Task<ISingleResponse<vmTransactionPrepareRequest>> PrepareTransactionRelatedRequestAsync(int currentUid)
        {
            var response = new SingleResponse<vmTransactionPrepareRequest>();

            try
            {
                // Retrieve PaymentMethods list
                response.Model.PaymentMethods = await PaymentMethodRepository.GetListPaymentMethodAsync();

                // Retrieve PaymentPayors list
                response.Model.PaymentPayors = await PaymentPayorRepository.GetListPaymentPayorAsync(currentUid);

                // Retrieve TransactionCategorys list
                response.Model.TransactionCategorys = await TransactionCategoryRepository.GetListTransactionCategoryAsync();

                // Retrieve TransactionTypes list
                response.Model.TransactionTypes = await TransactionTypeRepository.GetListTransactionTypeAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
    }
}
