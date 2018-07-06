using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Transactions
{
    public class TransactionCategory
    {
        public int TransactionCategoryID { get; set; }
        public string TransactionCategoryName { get; set; }
        public string TransactionCategoryDescription { get; set; }
        public bool IsFixed { get; set; }
        public bool IsRecurring { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<TransactionDetail> TransactionDetails { get; set; } = new Collection<TransactionDetail>();
    }
}
