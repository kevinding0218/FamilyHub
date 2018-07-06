using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        #region READ
        Task<Transaction> GetSingleLightTransactionByIDAsync(int transactionID);
        Task<Transaction> GetSingleLightTransactionByAmoutDescDateAsync(Double Amount, DateTime TransactionDate, String Description);
        Task<Transaction> GetSingleFullTransactionByIDAsync(int transactionID);

        Task<IEnumerable<Transaction>> GetListLightTransactionRangeAsync(int createdBy, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Transaction>> GetListFullTransactionRangeAsync(int createdBy, DateTime startDate, DateTime endDate);
        #endregion

        #region CREATE/UPDATE/DELETE
        Task<int> AddTransactionAsync(Transaction entity);
        Task<int> UpdateTransactionAsync(Transaction entity);
        Task<int> DeleteTransactionAsync(Transaction entity);
        #endregion
    }
}
