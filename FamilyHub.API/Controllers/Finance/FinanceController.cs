using FamilyHub.Data;
using FamilyHub.Data.Finance;
using FamilyHub.Service.Contracts;
using FamilyHub.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.Controllers.Finance
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/finance")]
    public class FinanceController : Controller
    {
        private readonly IUserInfo _userInfo;
        private readonly ITransactionsService _transactionsService;

        public FinanceController(
            IUserInfo userInfo,
            ITransactionsService transactionsService)
        {
            _userInfo = userInfo;
            _transactionsService = transactionsService;
        }

        #region Payment Payor
        [HttpPost("addPaymentPayor"), Authorize]
        public async Task<IActionResult> AddPaymentPayor()
        {
            var test = _userInfo;
            var userEmail = User.Identity.Name;

            //PaymentPayor newPaymentPayor = new PaymentPayor();
            //newPaymentPayor.PaymentPayorName = "Self";
            //newPaymentPayor.Active = true;
            //newPaymentPayor.PaymentSplit = false;

            //var relationship = await _paymentService.GetSinglePaymentPayorRelationshipAsync(7);
            //newPaymentPayor.PaymentPayorRelationshipFk = relationship.Model;
            ////newPaymentPayor.PaymentPayorRelationshipID = 7;
            //newPaymentPayor.CreatedBy = 1;
            //var saveResponse = await _paymentService.AddPaymentPayorAsync(newPaymentPayor);

            //return saveResponse.ToHttpResponse();
            return Ok();
        }
        #endregion

        #region Transaction
        [HttpGet("CreateTransactionRequest")]
        public async Task<IActionResult> GetCreateOrderRequestAsync(int uid)
        {
            // Get response from business logic
            var response = await _transactionsService.GetCreateTransactionRequestAsync(uid);

            // Return as http response
            return response.ToHttpResponse();
        }
        #endregion
    }
}
