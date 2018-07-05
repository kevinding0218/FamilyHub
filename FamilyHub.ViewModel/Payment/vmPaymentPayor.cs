using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Payment
{
    public class vmPaymentPayorCreateRequest
    {
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public int PaymentPayorRelationshipID { get; set; }
    }

    public class vmPaymentPayorUpdateRequest
    {
        public int PaymentPayorID { get; set; }
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public Boolean Active { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public int PaymentPayorRelationshipID { get; set; }
    }
}
