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
        #region Payment Payor
        Task<IListResponse<vmPaymentPayorListRequest>> GetListPaymentPayorAsync(int createdBy);
        Task<IResponse> AddPaymentPayorAsync(vmPaymentPayorCreateRequest newPaymentPayorRequest);
        Task<IResponse> UpdatePaymentPayorAsync(int paymentPayorID, vmPaymentPayorUpdateRequest updatePaymentPayorRequest);
        Task<IResponse> ToggleActivePaymentPayorAsync(int paymentPayorId, bool active);
        #endregion

        #region Payment Method
        Task<IListResponse<vmPaymentMethodListRequest>> GetListPaymentMethodAsync(int createdBy);
        Task<IListResponse<PaymentMethodType>> PreparePaymentMethodRelatedRequestAsync();
        Task<IResponse> AddPaymentMethodAsync(vmPaymentMethodCreateRequest newPaymentMethodRequest);
        Task<IResponse> UpdatePaymentMethodAsync(int paymentMethodId, vmPaymentMethodUpdateRequest updatePaymentMethodRequest);
        Task<IResponse> ToggleActivePaymentMethodAsync(int paymentMethodId, bool active);
        #endregion
    }
}
