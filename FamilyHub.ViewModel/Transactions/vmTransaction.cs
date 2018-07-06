using FamilyHub.Data.Payment;
using FamilyHub.Data.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Transactions
{
    public class vmTransactionListHttpRequest
    {
        public int UID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class vmTransactionListSimpleRequest
    {
        public DateTime? CreatedOn { get; set; }
        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public Double Amount { get; set; }
    }

    public class vmTransactionListFullRequest
    {
        public DateTime? CreatedOn { get; set; }
        public int TransactionID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public Double Amount { get; set; }

        public vmTransactionDetailContent TransactionDetailContent { get; set; }
    }

    public class vmTransactionDetailContent
    {
        public DateTime PostedDate { get; set; }
        public string TransactionTypeName { get; set; }
        public string TransactionCategoryName { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentPayorName { get; set; }
    }


    public class vmTransactionPrepareRequest
    {
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        public IEnumerable<TransactionCategory> TransactionCategorys { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
        public IEnumerable<PaymentPayor> PaymentPayors { get; set; }
    }

    public class vmTransactionCreateRequest
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionDescription { get; set; }
        public Double Amount { get; set; }

        public DateTime PostedDate { get; set; }
        public int TransactionTypeID { get; set; }
        public int TransactionCategoryID { get; set; }
        public int PaymentMethodID { get; set; }
        public int PaymentPayorID { get; set; }
    }

    public class vmTransactionUpdateRequest : vmTransactionCreateRequest
    {
        public int TransactionID { get; set; }
        public int TransactionDetailID { get; set; }
    }
}
