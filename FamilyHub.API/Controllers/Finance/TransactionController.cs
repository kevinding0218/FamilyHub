using FamilyHub.Data;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel.Transactions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Finance
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/trans")]
    public class TransactionController : Controller
    {
        private readonly IUserInfo _userInfo;
        private readonly ITransactionsService _transactionsService;

        public TransactionController(
            IUserInfo userInfo,
            ITransactionsService transactionsService)
        {
            _userInfo = userInfo;
            _transactionsService = transactionsService;
        }

        #region Transaction Category
        [HttpGet("getAllTransCate")]
        public async Task<IActionResult> GetAllTransactionCategoryAsync()
        {
            // Get response from business logic
            var response = await _transactionsService.GetListTransactionCategoryAsync();

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("createTransCate")]
        public async Task<IActionResult> CreateTransactionCategoryAsync([FromBody] vmTransactionCategoryCreateRequest newRequest)
        {
            // Get response from business logic
            var response = await _transactionsService.AddTransactionCategoryAsync(newRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPut("updateTransCate")]
        public async Task<IActionResult> UpdateTransactionCategoryAsync(int transactionCategoryId, [FromBody] vmTransactionCategoryUpdateRequest updateRequest)
        {
            // Get response from business logic
            var response = await _transactionsService.UpdateTransactionCategoryAsync(transactionCategoryId, updateRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("deleteTransCate")]
        public async Task<IActionResult> DeleteTransactionCategoryAsync(int transactionCategoryId)
        {
            // Get response from business logic
            var response = await _transactionsService.DeleteTransactionCategoryAsync(transactionCategoryId);

            // Return as http response
            return response.ToHttpResponse();
        }

        #endregion
        #region Transaction
        [HttpGet("getListTransSimple")]
        public async Task<IActionResult> GetListTransactionSimpleAsync([FromBody] vmTransactionListHttpRequest request)
        {
            // Get response from business logic
            var response = await _transactionsService.GetListTransactionSimpleRangeAsync(request.UID, request.StartDate, request.EndDate);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("getListTransFull")]
        public async Task<IActionResult> GetListTransactionFullAsync([FromBody] vmTransactionListHttpRequest request)
        {
            // Get response from business logic
            var response = await _transactionsService.GetListTransactionSimpleRangeAsync(request.UID, request.StartDate, request.EndDate);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpGet("prepareTransRelated")]
        public async Task<IActionResult> PrepareTransactionRelatedAsync(int uid)
        {
            // Get response from business logic
            var response = await _transactionsService.PrepareTransactionRelatedRequestAsync(uid);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPost("createTrans")]
        public async Task<IActionResult> CreateTransactionAsync([FromBody] vmTransactionCreateRequest newRequest)
        {
            // Get response from business logic
            var response = await _transactionsService.AddTransactionAsync(newRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpPut("updateTrans")]
        public async Task<IActionResult> UpdateTransactionAsync(int transactionId, [FromBody] vmTransactionUpdateRequest updateRequest)
        {
            // Get response from business logic
            var response = await _transactionsService.UpdateTransactionAsync(transactionId, updateRequest);

            // Return as http response
            return response.ToHttpResponse();
        }

        [HttpDelete("deleteTrans")]
        public async Task<IActionResult> DeleteTransactionAsync(int transactionID)
        {
            // Get response from business logic
            var response = await _transactionsService.DeleteTransactionAsync(transactionID);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion
    }
}
