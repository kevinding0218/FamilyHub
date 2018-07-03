using FamilyHub.Data;
using FamilyHub.Data.Transactions;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Transactions
{
    public class TransactionDetailRepository : Repository<TransactionDetail>, ITransactionDetailRepository
    {
        public TransactionDetailRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public TransactionDetailRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<TransactionDetail> GetSingleTransactionDetailByIDAsync(int transactionDetailID)
        {
            return await GetSingleOrDefaultAsync(
                    predicate: td => (td.TransactionDetailID == transactionDetailID),
                    include: (obj => (
                                    obj
                                    .Include(entity => entity.TransactionCategoryFk)
                                    .Include(entity => entity.TransactionTypeFk)
                                    .Include(entity => entity.PaymentPayorFk)
                                    .Include(entity => entity.PaymentMethodFk)
                            ))
                );
        }

        public async Task<IEnumerable<TransactionDetail>> GetListTransactionDetailAsync(int transactionID)
        {
            return await GetListAsync(
                    predicate: td => (td.TransactionFk.TransactionID == transactionID),
                    include: (obj => (
                                    obj
                                    .Include(entity => entity.TransactionCategoryFk)
                                    .Include(entity => entity.TransactionTypeFk)
                                    .Include(entity => entity.PaymentPayorFk)
                                    .Include(entity => entity.PaymentMethodFk)
                            ))
                );
        }

        public async Task<int> AddTransactionDetailAsync(TransactionDetail entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateTransactionDetailAsync(TransactionDetail entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }
    }
}
