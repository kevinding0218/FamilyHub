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
        #region READ
        Task<IListResponse<vmTransactionCategoryList>> GetListTransactionCategoryAsync();

        Task<IListResponse<vmTransactionListSimpleRequest>> GetListTransactionSimpleRangeAsync(int createdBy, DateTime startDate, DateTime endDate);
        Task<IListResponse<vmTransactionListFullRequest>> GetListTransactionFullRangeAsync(int createdBy, DateTime startDate, DateTime endDate);
        #endregion

        #region CREATE
        Task<IResponse> AddTransactionCategoryAsync(vmTransactionCategoryCreateRequest newTransactionCategoryRequest);

        Task<ISingleResponse<vmTransactionPrepareRequest>> PrepareTransactionRelatedRequestAsync(int currentUid);
        Task<IResponse> AddTransactionAsync(vmTransactionCreateRequest newTransactionRequest);
        #endregion

        #region UPDATE
        Task<IResponse> UpdateTransactionCategoryAsync(int transactionCategoryID, vmTransactionCategoryUpdateRequest updateTransactionCategoryRequest);

        Task<IResponse> UpdateTransactionAsync(int transactionID, vmTransactionUpdateRequest updateTransactionRequest);
        #endregion

        #region DELETE
        Task<IResponse> DeleteTransactionCategoryAsync(int transactionCategoryID);

        Task<IResponse> DeleteTransactionAsync(int transactionID);
        #endregion
    }
}
