using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class PaymentService : ServiceFactory, IPaymentService
    {
        public PaymentService(FamilyHubDbContext dbContext)
            : base(dbContext)
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
