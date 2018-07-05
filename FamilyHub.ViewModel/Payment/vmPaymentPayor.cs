using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Payment
{
    public class vmPaymentPayorListRequest
    {
        public int PaymentPayorID { get; set; }
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public Boolean Active { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public string PaymentPayorRelationshipName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class vmPaymentPayorCreateRequest
    {
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public int PaymentPayorRelationshipID { get; set; }
    }

    public class vmPaymentPayorUpdateRequest : vmPaymentPayorCreateRequest
    {
        public int PaymentPayorID { get; set; }
        public Boolean Active { get; set; }
    }
}
