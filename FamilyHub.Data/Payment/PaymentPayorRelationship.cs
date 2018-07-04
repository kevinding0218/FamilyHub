using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Payment
{
    public class PaymentPayorRelationship
    {
        public int PaymentPayorRelationshipID { get; set; }
        public string PaymentPayorRelationshipName { get; set; }
        public string PaymentPayorRelationshipDescription { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<PaymentPayor> PaymentPayors { get; set; } = new Collection<PaymentPayor>();
    }
}
