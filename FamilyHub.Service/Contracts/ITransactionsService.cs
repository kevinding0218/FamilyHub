using FamilyHub.Data.Payment;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Contracts
{
    public interface ITransactionsService
    {
        Task<ISingleResponse<vmTransactionCreateRequest>> GetCreateTransactionRequestAsync(int currentUid);
    }
}
