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
        Task<IListResponse<vmTransactionListSimpleRequest>> GetListTransactionSimpleAsync(int createdBy);
        Task<IListResponse<vmTransactionListFullRequest>> GetListTransactionFullAsync(int createdBy);
        Task<ISingleResponse<vmTransactionPrepareRequest>> PrepareTransactionRelatedRequestAsync(int currentUid);
        Task<IResponse> AddTransactionAsync(vmTransactionCreateRequest newTransactionRequest);
        Task<IResponse> UpdateTransactionAsync(int transactionID, vmTransactionUpdateRequest updateTransactionRequest);
        Task<IResponse> DeleteTransactionAsync(int transactionID);
    }
}
