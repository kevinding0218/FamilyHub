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
        public string PaymentPayorRelationshipName { get; set; }
        public Boolean Active { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ImageSource { get; set; }
    }

    public class vmPaymentPayorCreateRequest
    {
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public int MemberContactID { get; set; }
    }

    public class vmPaymentPayorUpdateRequest : vmPaymentPayorCreateRequest
    {
        public int PaymentPayorID { get; set; }
        public Boolean Active { get; set; }
    }
}
