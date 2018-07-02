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
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Transaction> GetSingleLightTransactionByIDAsync(int transactionID)
            => await GetSingleOrDefaultAsync(
                    predicate: tr => (tr.TransactionID == transactionID)
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

        public async Task<IEnumerable<Transaction>> GetListLightTransactionAsync(int createdBy)
            => await GetListAsync(
                    predicate: tr => (tr.CreatedBy == createdBy)
                );

        public async Task<IEnumerable<Transaction>> GetListFullTransactionAsync(int createdBy)
            => await GetListAsync(
                    predicate: tr => (tr.CreatedBy == createdBy),
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
    }
}
