using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Transactions
{
    public class TransactionType
    {
        public int TransactionTypeID { get; set; }
        public string TransactionTypeName { get; set; }
        public string TransactionTypeDescription { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<TransactionDetail> TransactionDetails { get; set; } = new Collection<TransactionDetail>();
    }
}
