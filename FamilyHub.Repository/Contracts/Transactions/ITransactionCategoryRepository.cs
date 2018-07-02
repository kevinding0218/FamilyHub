using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Transactions
{
    public interface ITransactionCategoryRepository : IRepository<TransactionCategory>
    {
        Task<TransactionCategory> GetSingleTransactionCategoryByIDAsync(int transactionCategoryID);
        Task<IEnumerable<TransactionCategory>> GetListTransactionCategoryAsync();
        Task<int> AddTransactionCategoryAsync(TransactionCategory entity);
        Task<int> UpdateTransactionCategoryAsync(TransactionCategory entity);
    }
}
