using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Transactions
{
    public class Transaction : IAuditableEntity
    {
        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public Double Amount { get; set; }
        public int TransactionDetailID { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual TransactionDetail TransactionDetailFk { get; set; }
    }
}
