using FamilyHub.Data.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Finance
{
    public interface IPaymentPayorRelationshipRepository : IRepository<PaymentPayorRelationship>
    {
        Task<PaymentPayorRelationship> GetSinglePaymentPayorRelationshipAsync(int paymentPayorRelationshipID);
    }
}
