using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        public async Task<PaymentPayor> GetSinglePaymentPayorByIDAsync(int paymentPayorID)
            => await GetSingleOrDefaultAsync(predicate: pp => pp.PaymentPayorID == paymentPayorID);

        public async Task<IEnumerable<PaymentPayor>> GetListPaymentPayorAsync(
            int createdBy,
            bool includeRelationship = false,
            bool includeTransactionDetails = false)
        {
            if (includeRelationship)
                return await GetListAsync(
                        predicate: pp => (pp.CreatedBy == createdBy),
                        include: (obj => obj.Include(entity => entity.PaymentPayorRelationshipFk))
                    );
            else if (includeTransactionDetails)
                return await GetListAsync(
                        predicate: pp => (pp.CreatedBy == createdBy),
                        include: (obj => obj.Include(entity => entity.TransactionDetails))
                    );
            else
                return await GetListAsync(
                        predicate: pp => (pp.CreatedBy == createdBy)
                    );
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
