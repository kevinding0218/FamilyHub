using FamilyHub.Data.Payment;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Payment;
using FamilyHub.ViewModel.Transactions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface IPaymentService
    {
        #region Payment Payor Relationship
        Task<IListResponse<PaymentPayorRelationship>> PreparePaymentPayorRelatedAsync();
        #endregion

        #region Payment Payor
        Task<ISingleResponse<PaymentPayor>> AddPaymentPayorAsync(PaymentPayor newPaymentPayor);
        #endregion

        #region Payment Method
        Task<IListResponse<PaymentMethodType>> PreparePaymentMethodRelatedRequestAsync();
        Task<IResponse> AddPaymentMethodAsync(vmPaymentMethodCreateRequest newPaymentMethodRequest);
        Task<IResponse> UpdatePaymentMethodAsync(int paymentMethodId, vmPaymentMethodUpdateRequest updatePaymentMethodRequest);
        Task<IResponse> ToggleActivePaymentMethodAsync(int paymentMethodId, bool active);
        #endregion
    }
}
