using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Finance
{
    public class PaymentPayor : IAuditableEntity, IActivateEntity
    {
        public int PaymentPayorID { get; set; }
        public string PaymentPayorName { get; set; }
        public string PaymentPayorDescription { get; set; }
        public Boolean Active { get; set; }
        public bool PaymentSplit { get; set; }
        public bool PaymentSplitFactor { get; set; }
        public int PaymentPayorRelationshipID { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual PaymentPayorRelationship PaymentPayorRelationshipFk { get; set; }
        public virtual Collection<TransactionDetail> TransactionDetails { get; set; } = new Collection<TransactionDetail>();
    }
}
