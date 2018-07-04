using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using FamilyHub.Service.Services;
using FamilyHub.ViewModel.Transactions;
using System;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class FinanceService : ServiceFactory, IFinanceService
    {
        public FinanceService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public FinanceService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public FinanceService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        #region Payment Payor
        public async Task<ISingleResponse<PaymentPayor>> AddPaymentPayorAsync(PaymentPayor newPaymentPayor)
        {
            var response = new SingleResponse<PaymentPayor>();

            try
            {
                // Create new user
                await PaymentPayorRepository.AddPaymentPayorAsync(newPaymentPayor);

                response.Message = ResponseMessageDisplay.Success;
                response.Model = newPaymentPayor;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region Payment Payor Relationship
        public async Task<ISingleResponse<PaymentPayorRelationship>> GetSinglePaymentPayorRelationshipAsync(int paymentPayorRelationshipID)
        {
            var response = new SingleResponse<PaymentPayorRelationship>();

            try
            {
                // Create new user
                var paymentPayorRelationshipFromDb = await PaymentPayorRelationshipRepository.GetSinglePaymentPayorRelationshipByIDAsync(paymentPayorRelationshipID);

                response.Message = ResponseMessageDisplay.Success;
                response.Model = paymentPayorRelationshipFromDb;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion
    }
}
