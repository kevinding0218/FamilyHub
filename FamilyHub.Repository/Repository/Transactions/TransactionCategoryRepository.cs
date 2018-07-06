using FamilyHub.Data;
using FamilyHub.Data.Transactions;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Transactions
{
    public class TransactionCategoryRepository : Repository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public TransactionCategoryRepository(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public async Task<TransactionCategory> GetSingleTransactionCategoryByIDAsync(int transactionCategoryID)
            => await GetSingleOrDefaultAsync(
                    predicate: tc => tc.TransactionCategoryID == transactionCategoryID
                );

        public async Task<TransactionCategory> GetSingleTransactionCategoryByNameAsync(string transactionCategoryName)
            => await GetSingleOrDefaultAsync(
                    predicate: tc => tc.TransactionCategoryName == transactionCategoryName
                );

        public async Task<IEnumerable<TransactionCategory>> GetListTransactionCategoryAsync()
            => await GetListAsync();

        public async Task<int> AddTransactionCategoryAsync(TransactionCategory entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> UpdateTransactionCategoryAsync(TransactionCategory entity)
        {
            Update(entity);

            return await CommitChangesAsync();
        }

        public async Task<int> DeleteTransactionCategoryAsync(TransactionCategory entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }
    }
}
