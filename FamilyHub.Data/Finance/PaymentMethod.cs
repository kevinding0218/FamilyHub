using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Finance
{
    public class PaymentMethod : IAuditableEntity
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodDescription { get; set; }
        public bool Active { get; set; }
        public int PaymentMethodTypeID { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual PaymentMethodType PaymentMethodTypeFk { get; set; }
        public virtual Collection<TransactionDetail> TransactionDetails { get; set; } = new Collection<TransactionDetail>();
    }
}
