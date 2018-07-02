using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Transactions
{
    public interface ITransactionTypeRepository : IRepository<TransactionType>
    {
        Task<IEnumerable<TransactionType>> GetListTransactionTypeAsync();
    }
}
