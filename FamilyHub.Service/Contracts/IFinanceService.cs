using FamilyHub.Data.Finance;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Transactions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface IFinanceService
    {
        #region Payment Payor Relationship
        Task<ISingleResponse<PaymentPayorRelationship>> GetSinglePaymentPayorRelationshipAsync(int paymentPayorRelationshipID);
        #endregion

        #region Payment Payor
        Task<ISingleResponse<PaymentPayor>> AddPaymentPayorAsync(PaymentPayor newPaymentPayor);
        #endregion
    }
}
