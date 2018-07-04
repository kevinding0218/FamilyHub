using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Payment
{
    public class PaymentMethodType
    {
        public int PaymentMethodTypeID { get; set; }
        public string PaymentMethodTypeName { get; set; }
        public string PaymentMethodTypeDescription { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<PaymentMethod> PaymentMethods { get; set; } = new Collection<PaymentMethod>();
    }
}
