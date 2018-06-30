using FamilyHub.Data.Finance;
using FamilyHub.Service.Responses;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface IFinanceService
    {
        Task<ISingleResponse<PaymentPayor>> AddPaymentPayorAsync(PaymentPayor newPaymentPayor);
        Task<ISingleResponse<PaymentPayorRelationship>> GetSinglePaymentPayorRelationshipAsync(int paymentPayorRelationshipID);
    }
}
