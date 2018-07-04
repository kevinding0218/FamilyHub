using FamilyHub.Data.Finance;
using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Transactions
{
    public class vmCreateTransactionRequest
    {
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        public IEnumerable<TransactionCategory> TransactionCategorys { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<PaymentPayor> PaymentPayors { get; set; }
    }
}
