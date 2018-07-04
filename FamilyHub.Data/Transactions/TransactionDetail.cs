using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Transactions
{
    public class TransactionDetail
    {
        public int TransactionDetailID { get; set; }
        public DateTime PostedDate { get; set; }
        public int TransactionTypeID { get; set; }
        public int TransactionCategoryID { get; set; }
        public int PaymentMethodID { get; set; }
        public int PaymentPayorID { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual TransactionType TransactionTypeFk { get; set; }
        public virtual TransactionCategory TransactionCategoryFk { get; set; }
        public virtual PaymentMethod PaymentMethodFk { get; set; }
        public virtual PaymentPayor PaymentPayorFk { get; set; }
        public virtual Transaction TransactionFk { get; set; }
    }
}
