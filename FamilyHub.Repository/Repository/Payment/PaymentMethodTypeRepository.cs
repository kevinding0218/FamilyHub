using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Payment
{
    public class PaymentMethodTypeRepository : Repository<PaymentMethodType>, IPaymentMethodTypeRepository
    {
        public PaymentMethodTypeRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public PaymentMethodTypeRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<IEnumerable<PaymentMethodType>> GetListPaymentMethodTypeAsync()
                => await GetListAsync();
    }
}
