﻿using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Payment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Payment
{
    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public PaymentMethodRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<PaymentMethod> GetSinglePaymentMethodByIDAsync(int paymentMethodID)
            => await GetSingleOrDefaultAsync(predicate: pm => pm.PaymentMethodID == paymentMethodID);

        public async Task<PaymentMethod> GetSinglePaymentMethodByNameAsync(string paymentMethodName)
            => await GetSingleOrDefaultAsync(predicate: pm => pm.PaymentMethodName == paymentMethodName);

        public async Task<IEnumerable<PaymentMethod>> GetListPaymentMethodAsync(int createdBy = 0, int paymentMethodTypeId = 0, bool active = true)
        {
            if (createdBy > 0)
            {
                return await GetListAsync(
                        predicate: (p => p.CreatedBy == createdBy && p.Active == active)
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

        public async Task ActivatePaymentMethodAsync(PaymentMethod entity)
        {
            Activate(entity);

            await CommitChangesAsync();
        }

        public async Task DeactivatePaymentMethodAsync(PaymentMethod entity)
        {
            Deactive(entity);

            await CommitChangesAsync();
        }
    }
}
