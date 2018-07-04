using FamilyHub.Data.Payment;
using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Transactions
{
    public class vmTransactionCreateRequest
    {
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        public IEnumerable<TransactionCategory> TransactionCategorys { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<PaymentPayor> PaymentPayors { get; set; }
    }
}
