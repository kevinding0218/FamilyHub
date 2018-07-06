using FamilyHub.Data;
using FamilyHub.Data.Transactions;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Transactions
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public TransactionRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<Transaction> GetSingleLightTransactionByIDAsync(int transactionID)
            => await GetSingleOrDefaultAsync(
                    predicate: tr => (tr.TransactionID == transactionID)
                );

        public async Task<Transaction> GetSingleLightTransactionByAmoutDescDateAsync(Double Amount, DateTime TransactionDate, String Description)
            => await GetSingleOrDefaultAsync(
                    predicate: tr => (tr.Amount == Amount && DateTime.Compare(tr.TransactionDate, TransactionDate) == 0 && tr.TransactionDescription == Description)
                );

        public async Task<Transaction> GetSingleFullTransactionByIDAsync(int transactionID)
            => await GetSingleOrDefaultAsync(
                    predicate: tr => (tr.TransactionID == transactionID),
                    include: (obj => (
                                    obj
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.PaymentPayorFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.PaymentMethodFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.TransactionTypeFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.TransactionCategoryFk)
                            ))
                );

        public async Task<IEnumerable<Transaction>> GetListLightTransactionRangeAsync(int createdBy, DateTime startDate, DateTime endDate)
            => await GetListAsync(
                    predicate: tr => (tr.CreatedBy == createdBy
                        && tr.TransactionDate >= startDate
                        && tr.TransactionDate < endDate
                    ),
                    orderBy: tr => tr.OrderBy(entity => entity.TransactionDate)
                );

        public async Task<IEnumerable<Transaction>> GetListFullTransactionRangeAsync(int createdBy, DateTime startDate, DateTime endDate)
            => await GetListAsync(
                    predicate: tr => (tr.CreatedBy == createdBy
                        && tr.TransactionDate >= startDate
                        && tr.TransactionDate < endDate
                    ),
                    orderBy: tr => tr.OrderBy(entity => entity.TransactionDate),
                    include: (obj => (
                                    obj
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.PaymentPayorFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.PaymentMethodFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.TransactionTypeFk)
                                    .Include(entity => entity.TransactionDetailFk)
                                        .ThenInclude(td => td.TransactionCategoryFk)
                            ))
                );

        public async Task<int> AddTransactionAsync(Transaction entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateTransactionAsync(Transaction entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteTransactionAsync(Transaction entity)
        {
            Deactive(entity);

            return await CommitChangesAsync();
        }
    }
}
