using FamilyHub.Data.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Finance
{
    public interface IPaymentPayorRepository : IRepository<PaymentPayor>
    {
        Task<IEnumerable<PaymentPayor>> GetPaymentPayorListAsync(
            int createdBy,
            bool includeRelationship = false,
            bool includeTransactionDetails = false);
        Task<Int32> AddPaymentPayorAsync(PaymentPayor entity);
        Task<Int32> UpdatePaymentPayorAsync(PaymentPayor entity);
        Task DeactivatePaymentPayorAsync(PaymentPayor entity);
    }
}
