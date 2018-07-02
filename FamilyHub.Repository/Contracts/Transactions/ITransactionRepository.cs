using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<Transaction> GetSingleLightTransactionByIDAsync(int transactionID);
        Task<Transaction> GetSingleFullTransactionByIDAsync(int transactionID);
        Task<IEnumerable<Transaction>> GetListLightTransactionAsync(int createdBy);
        Task<IEnumerable<Transaction>> GetListFullTransactionAsync(int createdBy);
        Task<int> AddTransactionAsync(Transaction entity);
        Task<int> UpdateTransactionAsync(Transaction entity);
    }
}
