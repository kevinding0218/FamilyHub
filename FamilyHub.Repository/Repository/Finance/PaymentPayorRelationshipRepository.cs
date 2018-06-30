using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Finance
{
    public class PaymentPayorRelationshipRepository : Repository<PaymentPayorRelationship>, IPaymentPayorRelationshipRepository
    {
        public PaymentPayorRelationshipRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<PaymentPayorRelationship> GetSinglePaymentPayorRelationshipByIDAsync(int paymentPayorRelationshipID)
            => await GetSingleOrDefaultAsync(predicate: pr => pr.PaymentPayorRelationshipID == paymentPayorRelationshipID);

        public async Task<IEnumerable<PaymentPayorRelationship>> GetListPaymentPayorRelationshipAsync()
            => await GetListAsync();

    }
}
