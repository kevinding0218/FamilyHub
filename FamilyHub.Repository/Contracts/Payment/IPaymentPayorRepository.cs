using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Payment
{
    public interface IPaymentPayorRepository : IRepository<PaymentPayor>
    {
        Task<PaymentPayor> GetSinglePaymentPayorByIDAsync(int paymentPayorID);
        Task<PaymentPayor> GetSinglePaymentPayorByNameAsync(string paymentPayorName);
        Task<IEnumerable<PaymentPayor>> GetListPaymentPayorAsync(
            int createdBy,
            bool includeRelationship = false,
            bool includeTransactionDetails = false);
        Task<Int32> AddPaymentPayorAsync(PaymentPayor entity);
        Task<Int32> UpdatePaymentPayorAsync(PaymentPayor entity);
        Task ActivatePaymentPayorAsync(PaymentPayor entity);
        Task DeactivatePaymentPayorAsync(PaymentPayor entity);
    }
}
