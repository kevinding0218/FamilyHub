using FamilyHub.Data.Member;
using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Payment
{
    public class PaymentPayor : IAuditableEntity, IActivateEntity
    {
        public int PaymentPayorID { get; set; }
        public int MemberContactID { get; set; }
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

        public virtual MemberContact MemberContactFk { get; set; }
        public virtual Collection<TransactionDetail> TransactionDetails { get; set; } = new Collection<TransactionDetail>();
    }
}
