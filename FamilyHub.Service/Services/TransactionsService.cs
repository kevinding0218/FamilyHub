using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.Data.Transactions;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using FamilyHub.ViewModel.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class TransactionsService : ServiceFactory, ITransactionsService
    {
        public TransactionsService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public TransactionsService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public TransactionsService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        #region READ
        public async Task<IListResponse<vmTransactionCategoryList>> GetListTransactionCategoryAsync()
        {
            var response = new ListResponse<vmTransactionCategoryList>();

            try
            {
                var listTransactionCategoryFromDb = await TransactionCategoryRepository.GetListTransactionCategoryAsync();
                response.Model = _mapper.Map<IEnumerable<TransactionCategory>, IEnumerable<vmTransactionCategoryList>>(listTransactionCategoryFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IListResponse<vmTransactionListSimpleRequest>> GetListTransactionSimpleRangeAsync(int createdBy, DateTime startDate, DateTime endDate)
        {
            var response = new ListResponse<vmTransactionListSimpleRequest>();

            try
            {
                var listTransactionFromDb = await TransactionRepository.GetListLightTransactionRangeAsync(createdBy, startDate, endDate);
                response.Model = _mapper.Map<IEnumerable<Transaction>, IEnumerable<vmTransactionListSimpleRequest>>(listTransactionFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IListResponse<vmTransactionListFullRequest>> GetListTransactionFullRangeAsync(int createdBy, DateTime startDate, DateTime endDate)
        {
            var response = new ListResponse<vmTransactionListFullRequest>();

            try
            {
                var listTransactionFromDb = await TransactionRepository.GetListFullTransactionRangeAsync(createdBy, startDate, endDate);
                response.Model = _mapper.Map<IEnumerable<Transaction>, IEnumerable<vmTransactionListFullRequest>>(listTransactionFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region CREATE
        public async Task<IResponse> AddTransactionCategoryAsync(vmTransactionCategoryCreateRequest newTransactionCategoryRequest)
        {
            var response = new Response();

            try
            {
                var newTransactionCategory = _mapper.Map<vmTransactionCategoryCreateRequest, TransactionCategory>(newTransactionCategoryRequest);
                // Create new payment payor
                await TransactionCategoryRepository.AddTransactionCategoryAsync(newTransactionCategory);

                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<ISingleResponse<vmTransactionPrepareRequest>> PrepareTransactionRelatedRequestAsync(int currentUid)
        {
            var response = new SingleResponse<vmTransactionPrepareRequest>();

            try
            {
                // Retrieve PaymentMethods list
                response.Model.PaymentMethods = await PaymentMethodRepository.GetListPaymentMethodAsync();

                // Retrieve PaymentPayors list
                response.Model.PaymentPayors = await PaymentPayorRepository.GetListPaymentPayorAsync(currentUid);

                // Retrieve TransactionCategorys list
                response.Model.TransactionCategorys = await TransactionCategoryRepository.GetListTransactionCategoryAsync();

                // Retrieve TransactionTypes list
                response.Model.TransactionTypes = await TransactionTypeRepository.GetListTransactionTypeAsync();
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> AddTransactionAsync(vmTransactionCreateRequest newTransactionRequest)
        {
            var response = new Response();

            try
            {
                var newTransaction = _mapper.Map<vmTransactionCreateRequest, Transaction>(newTransactionRequest);
                // Create new payment payor
                await TransactionRepository.AddTransactionAsync(newTransaction);



                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            #region Using Sql Transaction
            //try
            //{
            //    // Begin transaction
            //    using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            //    {
            //        // Retrieve warehouses
            //        var transactionDetailToSave = _mapper.Map<vmTransactionCreateRequest, TransactionDetail>(newTransactionRequest);

            //        try
            //        {
            //            var newTransactionDetailID = await TransactionDetailRepository.AddTransactionDetailAsync(transactionDetailToSave);

            //            var newTransactionToSave = _mapper.Map<vmTransactionCreateRequest, Transaction>(newTransactionRequest);
            //            newTransactionToSave.TransactionDetailID = newTransactionDetailID;

            //            await TransactionRepository.AddTransactionAsync(newTransactionToSave);

            //            // Commit transaction
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.Rollback();

            //            throw ex;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    response.SetError(ex);
            //}
            #endregion

            return response;
        }
        #endregion

        #region UPDATE
        public async Task<IResponse> UpdateTransactionCategoryAsync(int transactionCategoryID, vmTransactionCategoryUpdateRequest updateTransactionCategoryRequest)
        {
            var response = new Response();

            try
            {
                var duplicateTransactionCategory = await TransactionCategoryRepository.GetSingleTransactionCategoryByNameAsync(updateTransactionCategoryRequest.TransactionCategoryName);

                if (duplicateTransactionCategory != null && duplicateTransactionCategory.TransactionCategoryID != transactionCategoryID)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionCategoryAlreadyExistedMessage, updateTransactionCategoryRequest.TransactionCategoryName));
                }
                else
                {
                    var TransactionCategoryFromDB = await TransactionCategoryRepository.GetSingleTransactionCategoryByIDAsync(transactionCategoryID);
                    if (TransactionCategoryFromDB == null)
                    {
                        response.Message = ResponseMessageDisplay.NotFound;
                        // Throw exception if duplicate existed
                        throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionCategoryNotFoundMessage));
                    }
                    else
                    {
                        _mapper.Map<vmTransactionCategoryUpdateRequest, TransactionCategory>(updateTransactionCategoryRequest, TransactionCategoryFromDB);
                        await TransactionCategoryRepository.UpdateTransactionCategoryAsync(TransactionCategoryFromDB);

                        response.Message = ResponseMessageDisplay.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> CheckDuplicateTransactionAsync(int TransactionID, Double Amount, DateTime TransactionDate, String TransactionDescription)
        {
            var response = new Response();

            try
            {
                var duplicateTransaction = await TransactionRepository.GetSingleLightTransactionByAmoutDescDateAsync(
                    Amount,
                    TransactionDate,
                    TransactionDescription);

                if (duplicateTransaction != null && duplicateTransaction.TransactionID != TransactionID)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionAlreadyExistedMessage,
                        Amount,
                        TransactionDate,
                        TransactionDescription));
                }
                else
                {
                    response.Message = ResponseMessageDisplay.Valid;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> UpdateTransactionAsync(int transactionID, vmTransactionUpdateRequest updateTransactionRequest)
        {
            var response = new Response();

            try
            {
                var transactionFromDB = await TransactionRepository.GetSingleLightTransactionByIDAsync(transactionID);
                if (transactionFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionNotFoundMessage));
                }
                else
                {
                    _mapper.Map<vmTransactionUpdateRequest, Transaction>(updateTransactionRequest, transactionFromDB);
                    await TransactionRepository.UpdateTransactionAsync(transactionFromDB);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region DELETE
        public async Task<IResponse> DeleteTransactionAsync(int transactionID)
        {
            var response = new Response();

            try
            {
                var transactionFromDB = await TransactionRepository.GetSingleLightTransactionByIDAsync(transactionID);
                if (transactionFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionNotFoundMessage));
                }
                else
                {
                    await TransactionRepository.DeleteTransactionAsync(transactionFromDB);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> DeleteTransactionCategoryAsync(int transactionCategoryID)
        {
            var response = new Response();

            try
            {
                var transactionCategoryFromDB = await TransactionCategoryRepository.GetSingleTransactionCategoryByIDAsync(transactionCategoryID);
                if (transactionCategoryFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(TransactionMessageDisplay.TransactionCategoryNotFoundMessage));
                }
                else
                {
                    await TransactionCategoryRepository.DeleteTransactionCategoryAsync(transactionCategoryFromDB);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion
    }
}
