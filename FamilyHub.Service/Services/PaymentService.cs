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
        public async Task<ISingleResponse<PaymentPayor>> AddPaymentPayorAsync(PaymentPayor newPaymentPayor)
        {
            var response = new SingleResponse<PaymentPayor>();

            try
            {
                // Create new user
                await PaymentPayorRepository.AddPaymentPayorAsync(newPaymentPayor);

                response.Message = ResponseMessageDisplay.Success;
                response.Model = newPaymentPayor;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region Payment Payor Relationship
        public async Task<IListResponse<PaymentPayorRelationship>> PreparePaymentPayorRelatedAsync()
        {
            var response = new ListResponse<PaymentPayorRelationship>();

            try
            {
                // Create new user
                response.Model = await PaymentPayorRelationshipRepository.GetListPaymentPayorRelationshipAsync();
                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        #endregion

        #region Payment Method
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
                var newPaymentMethod = _mapper.Map<vmPaymentMethodCreateRequest, PaymentMethod>(newPaymentMethodRequest);
                // Create new payment method
                await PaymentMethodRepository.AddPaymentMethodAsync(newPaymentMethod);

                response.Message = ResponseMessageDisplay.Success;
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }

        public async Task<IResponse> UpdatePaymentMethodAsync(int paymentMethodId, vmPaymentMethodUpdateRequest updatePaymentMethodRequest)
        {
            var response = new Response();

            try
            {
                var duplicatePaymentMethod = await PaymentMethodRepository.GetSinglePaymentMethodByNameAsync(updatePaymentMethodRequest.PaymentMethodName);

                if (duplicatePaymentMethod != null)
                {
                    response.Message = ResponseMessageDisplay.Duplicate;
                    // Throw exception if duplicate email account existed
                    throw new FamilyHubException(string.Format(PaymentMessageDisplay.PaymentMethodAlreadyExistedMessage, updatePaymentMethodRequest.PaymentMethodName));
                }
                else
                {
                    var paymentMethodFromDB = await PaymentMethodRepository.GetSinglePaymentMethodByIDAsync(paymentMethodId);
                    if (paymentMethodFromDB == null)
                    {
                        response.Message = ResponseMessageDisplay.NotFound;
                        // Throw exception if duplicate email account existed
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
                    // Throw exception if duplicate email account existed
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
