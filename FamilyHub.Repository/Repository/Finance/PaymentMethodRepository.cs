using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Finance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Finance
{
    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodListAsync(int CreatedByUid = 0, int paymentMethodTypeId = 0, bool active = true)
        {
            if (CreatedByUid > 0)
            {
                return await GetListAsync(
                        predicate: (p => p.CreatedBy == CreatedByUid && p.Active == active)
                    );
            }
            else if (paymentMethodTypeId > 0)
            {
                return await GetListAsync(
                        predicate: (p => p.PaymentMethodTypeID == paymentMethodTypeId && p.Active == active)
                    );
            }
            else
            {
                return await GetListAsync(
                        predicate: (p => p.Active == active)
                    );
            }
        }

        public async Task<int> AddPaymentMethodAsync(PaymentMethod entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdatePaymentMethodAsync(PaymentMethod entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }

        public async Task DeactivatePaymentMethodAsync(PaymentMethod entity)
        {
            Deactive(entity);

            await CommitChangesAsync();
        }
    }
}
