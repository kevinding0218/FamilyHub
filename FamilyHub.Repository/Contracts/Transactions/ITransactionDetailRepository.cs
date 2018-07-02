using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Transactions
{
    public interface ITransactionDetailRepository : IRepository<TransactionDetail>
    {
        Task<TransactionDetail> GetSingleTransactionDetailByIDAsync(int transactionDetailID);
        Task<IEnumerable<TransactionDetail>> GetListTransactionDetailAsync(int transactionID);
        Task<int> AddTransactionDetailAsync(TransactionDetail entity);
        Task<int> UpdateTransactionDetailAsync(TransactionDetail entity);
    }
}
