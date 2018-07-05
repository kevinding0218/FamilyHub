using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Payment
{
    public class vmPaymentMethodListRequest
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodDescription { get; set; }
        public Boolean Active { get; set; }
        public string PaymentMethodTypeName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class vmPaymentMethodCreateRequest
    {
        public string PaymentMethodName { get; set; }
        public string PaymentMethodDescription { get; set; }
        public int PaymentMethodTypeID { get; set; }
    }

    public class vmPaymentMethodUpdateRequest : vmPaymentMethodCreateRequest
    {
        public int PaymentMethodID { get; set; }
        public Boolean Active { get; set; }
    }
}
