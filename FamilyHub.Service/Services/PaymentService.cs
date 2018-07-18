using AutoMapper;
using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Exceptions;
using FamilyHub.Service.Responses;
using FamilyHub.Service.Services;
using FamilyHub.ViewModel.Payment;
using FamilyHub.ViewModel.Transactions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class PaymentService : ServiceFactory, IPaymentService
    {
        public PaymentService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public PaymentService(IMapper mapper, FamilyHubDbContext dbContext)
            : base(mapper, dbContext)
        {
        }

        public PaymentService(IMapper mapper, IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(mapper, userInfo, dbContext)
        {
        }

        #region Payment Payor
        public async Task<IListResponse<vmPaymentPayorListRequest>> GetListPaymentPayorAsync(int createdBy)
        {
            var response = new ListResponse<vmPaymentPayorListRequest>();

            try
            {
                var listPaymentPayorFromDb = await PaymentPayorRepository.GetListPaymentPayorAsync(createdBy, includeRelationship: true);
                response.Model = _mapper.Map<IEnumerable<PaymentPayor>, IEnumerable<vmPaymentPayorListRequest>>(listPaymentPayorFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> AddPaymentPayorAsync(vmPaymentPayorCreateRequest newPaymentPayorRequest)
        {
            var response = new Response();

            try
            {
                var newPaymentPayor = _mapper.Map<vmPaymentPayorCreateRequest, PaymentPayor>(newPaymentPayorRequest);
                // Create new payment payor
                await PaymentPayorRepository.AddPaymentPayorAsync(newPaymentPayor);

                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> UpdatePaymentPayorAsync(int paymentPayorID, vmPaymentPayorUpdateRequest updatePaymentPayorRequest)
        {
            var response = new Response();

            try
            {
                var paymentPayorFromDB = await PaymentPayorRepository.GetSinglePaymentPayorByIDAsync(paymentPayorID);
                if (paymentPayorFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentPayorNotFoundMessage));
                }
                else
                {
                    _mapper.Map<vmPaymentPayorUpdateRequest, PaymentPayor>(updatePaymentPayorRequest, paymentPayorFromDB);
                    await PaymentPayorRepository.UpdatePaymentPayorAsync(paymentPayorFromDB);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> ToggleActivePaymentPayorAsync(int paymentPayorId, bool active)
        {
            var response = new Response();

            try
            {
                var paymentPayorFromDB = await PaymentPayorRepository.GetSinglePaymentPayorByIDAsync(paymentPayorId);
                if (paymentPayorFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentPayorNotFoundMessage));
                }
                else
                {
                    if (active) await PaymentPayorRepository.ActivatePaymentPayorAsync(paymentPayorFromDB);
                    else await PaymentPayorRepository.DeactivatePaymentPayorAsync(paymentPayorFromDB);

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

        #region Payment Payor Relationship

        #endregion

        #region Payment Method
        public async Task<IListResponse<vmPaymentMethodListRequest>> GetListPaymentMethodAsync(int createdBy)
        {
            var response = new ListResponse<vmPaymentMethodListRequest>();

            try
            {
                var listPaymentMethodFromDb = await PaymentMethodRepository.GetListPaymentMethodAsync(createdBy);
                response.Model = _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<vmPaymentMethodListRequest>>(listPaymentMethodFromDb);
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IListResponse<PaymentMethodType>> PreparePaymentMethodRelatedRequestAsync()
        {
            var response = new ListResponse<PaymentMethodType>();

            try
            {
                response.Model = await PaymentMethodTypeRepository.GetListPaymentMethodTypeAsync();
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> AddPaymentMethodAsync(vmPaymentMethodCreateRequest newPaymentMethodRequest)
        {
            var response = new Response();

            try
            {
                var duplicatePaymentMethod = await PaymentMethodRepository.GetSinglePaymentMethodByNameAsync(newPaymentMethodRequest.PaymentMethodName);

                if (duplicatePaymentMethod != null)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentMethodAlreadyExistedMessage, newPaymentMethodRequest.PaymentMethodName));
                }
                else
                {
                    var newPaymentMethod = _mapper.Map<vmPaymentMethodCreateRequest, PaymentMethod>(newPaymentMethodRequest);
                    // Create new payment method
                    await PaymentMethodRepository.AddPaymentMethodAsync(newPaymentMethod);

                    response.Message = ResponseMessageDisplay.Success;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> UpdatePaymentMethodAsync(int paymentMethodID, vmPaymentMethodUpdateRequest updatePaymentMethodRequest)
        {
            var response = new Response();

            try
            {
                var duplicatePaymentMethod = await PaymentMethodRepository.GetSinglePaymentMethodByNameAsync(updatePaymentMethodRequest.PaymentMethodName);

                if (duplicatePaymentMethod != null && duplicatePaymentMethod.PaymentMethodID != paymentMethodID)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentMethodAlreadyExistedMessage, updatePaymentMethodRequest.PaymentMethodName));
                }
                else
                {
                    var paymentMethodFromDB = await PaymentMethodRepository.GetSinglePaymentMethodByIDAsync(paymentMethodID);
                    if (paymentMethodFromDB == null)
                    {
                        response.Message = ResponseMessageDisplay.NotFound;
                        // Throw exception if duplicate existed
                        throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentMethodNotFoundMessage));
                    }
                    else
                    {
                        _mapper.Map<vmPaymentMethodUpdateRequest, PaymentMethod>(updatePaymentMethodRequest, paymentMethodFromDB);
                        await PaymentMethodRepository.UpdatePaymentMethodAsync(paymentMethodFromDB);

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

        public async Task<IResponse> ToggleActivePaymentMethodAsync(int paymentMethodId, bool active)
        {
            var response = new Response();

            try
            {
                var paymentMethodFromDB = await PaymentMethodRepository.GetSinglePaymentMethodByIDAsync(paymentMethodId);
                if (paymentMethodFromDB == null)
                {
                    response.Message = ResponseMessageDisplay.NotFound;
                    // Throw exception if duplicate existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentMethodNotFoundMessage));
                }
                else
                {
                    if (active) await PaymentMethodRepository.ActivatePaymentMethodAsync(paymentMethodFromDB);
                    else await PaymentMethodRepository.DeactivatePaymentMethodAsync(paymentMethodFromDB);

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
