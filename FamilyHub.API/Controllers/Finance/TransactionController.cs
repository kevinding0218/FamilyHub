using FamilyHub.Data;
using FamilyHub.Service.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Finance
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/transaction")]
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

        #region Transaction
        [HttpGet("prepareTransactionRelated")]
        public async Task<IActionResult> PrepareTransactionRelatedAsync(int uid)
        {
            // Get response from business logic
            var response = await _transactionsService.PrepareTransactionRelatedRequestAsync(uid);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion
    }
}
