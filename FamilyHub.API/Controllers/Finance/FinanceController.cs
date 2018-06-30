using FamilyHub.Data.Finance;
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
    [Route("/api/finance")]
    public class FinanceController
    {
        private readonly IFinanceService _financeService;

        public FinanceController(
            IFinanceService financeService)
        {
            _financeService = financeService;
        }

        [HttpPost("addpaymentpayor")]
        public async Task<IActionResult> AddPaymentPayor()
        {
            PaymentPayor newPaymentPayor = new PaymentPayor();
            newPaymentPayor.PaymentPayorName = "Self";
            newPaymentPayor.Active = true;
            newPaymentPayor.PaymentSplit = false;

            var relationship = await _financeService.GetSinglePaymentPayorRelationshipAsync(7);
            newPaymentPayor.PaymentPayorRelationshipFk = relationship.Model;
            //newPaymentPayor.PaymentPayorRelationshipID = 7;
            newPaymentPayor.CreatedBy = 1;
            var saveResponse = await _financeService.AddPaymentPayorAsync(newPaymentPayor);

            return saveResponse.ToHttpResponse();
        }
    }
}
