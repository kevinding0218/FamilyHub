using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Finance
{
    public class PaymentPayorRepository : Repository<PaymentPayor>, IPaymentPayorRepository
    {
        public PaymentPayorRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<int> AddPaymentPayorAsync(PaymentPayor entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdatePaymentPayorAsync(PaymentPayor entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }

        public async Task DeactivatePaymentPayorAsync(PaymentPayor entity)
        {
            Deactive(entity);

            await CommitChangesAsync();
        }
    }
}
