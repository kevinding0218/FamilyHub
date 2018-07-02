using FamilyHub.Data.Transactions;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Transactions
{
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<TransactionType>> GetListTransactionTypeAsync() => await GetListAsync();
    }
}
