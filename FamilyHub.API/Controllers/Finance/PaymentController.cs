using FamilyHub.Data;
using FamilyHub.Data.Payment;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel;
using FamilyHub.ViewModel.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Payment
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/payment")]
    public class PaymentController : Controller
    {
        private readonly IUserInfo _userInfo;
        private readonly IPaymentService _paymentService;

        public PaymentController(
            IUserInfo userInfo,
            IPaymentService paymentService)
        {
            _userInfo = userInfo;
            _paymentService = paymentService;
        }

        #region Payment Method
        [HttpGet("preparePaymentMethodRelated")]
        public async Task<IActionResult> PreparePaymentMethodRelatedAsync()
        {
            // Get response from business logic
            var response = await _paymentService.PreparePaymentMethodRelatedRequestAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("createPaymentMethod")]
        public async Task<IActionResult> CreatePaymentMethodAsync([FromBody] vmPaymentMethodCreateRequest newPaymentMethodRequest)
        {
            // Get response from business logic
            var response = await _paymentService.AddPaymentMethodAsync(newPaymentMethodRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPut("updatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethodAsync(int paymentMethodId, [FromBody] vmPaymentMethodUpdateRequest updatePaymentMethodRequest)
        {
            // Get response from business logic
            var response = await _paymentService.UpdatePaymentMethodAsync(paymentMethodId, updatePaymentMethodRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPatch("activePaymentMethod")]
        public async Task<IActionResult> ActivePaymentMethodAsync(int paymentMethodId)
        {
            // Get response from business logic
            var response = await _paymentService.ToggleActivePaymentMethodAsync(paymentMethodId, true);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("deactivePaymentMethod")]
        public async Task<IActionResult> DeactivePaymentMethodAsync(int paymentMethodId)
        {
            // Get response from business logic
            var response = await _paymentService.ToggleActivePaymentMethodAsync(paymentMethodId, false);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion

        #region Payment Payor
        [HttpGet("preparePaymentPayorRelated")]
        public async Task<IActionResult> preparePaymentPayorRelatedAsync()
        {
            // Get response from business logic
            var response = await _paymentService.PreparePaymentPayorRelatedAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("createPaymentPayor")]
        public async Task<IActionResult> CreatePaymentPayorAsync([FromBody] vmPaymentPayorCreateRequest newPaymentPayorRequest)
        {
            // Get response from business logic
            var response = await _paymentService.AddPaymentPayorAsync(newPaymentPayorRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPut("updatePaymentPayor")]
        public async Task<IActionResult> UpdatePaymentPayorAsync(int paymentPayorId, [FromBody] vmPaymentPayorUpdateRequest updatePaymentPayorRequest)
        {
            // Get response from business logic
            var response = await _paymentService.UpdatePaymentPayorAsync(paymentPayorId, updatePaymentPayorRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPatch("activePaymentPayor")]
        public async Task<IActionResult> ActivePaymentPayorAsync(int paymentPayorId)
        {
            // Get response from business logic
            var response = await _paymentService.ToggleActivePaymentPayorAsync(paymentPayorId, true);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("deactivePaymentPayor")]
        public async Task<IActionResult> DeactivePaymentPayorAsync(int paymentPayorId)
        {
            // Get response from business logic
            var response = await _paymentService.ToggleActivePaymentPayorAsync(paymentPayorId, false);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion


    }
}
