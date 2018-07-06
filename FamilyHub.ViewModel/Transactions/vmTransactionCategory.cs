using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Transactions
{
    public class vmTransactionCategoryCreateRequest
    {
        public string TransactionCategoryName { get; set; }
        public string TransactionCategoryDescription { get; set; }
        public bool IsFixed { get; set; }
        public bool IsRecurring { get; set; }
    }

    public class vmTransactionCategoryList : vmTransactionCategoryCreateRequest
    {
        public int TransactionCategoryID { get; set; }
    }

    public class vmTransactionCategoryUpdateRequest : vmTransactionCategoryCreateRequest
    {
        public int TransactionCategoryID { get; set; }
    }
}
