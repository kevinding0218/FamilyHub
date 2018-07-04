using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Payment
{
    public interface IPaymentPayorRelationshipRepository : IRepository<PaymentPayorRelationship>
    {
        Task<PaymentPayorRelationship> GetSinglePaymentPayorRelationshipByIDAsync(int paymentPayorRelationshipID);
        Task<IEnumerable<PaymentPayorRelationship>> GetListPaymentPayorRelationshipAsync();
    }
}
