using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Display
{
    public class TransactionMessageDisplay
    {
        public static string TransactionCategoryNotFoundMessage
            => "Transaction Category not found.";
        public static string TransactionCategoryAlreadyExistedMessage
            => "Transaction Category '{0}' has already been existed. Please choose a different one.";

        public static string TransactionNotFoundMessage
            => "Transaction not found.";
        public static string TransactionAlreadyExistedMessage
            => "Transaction Amount of '{0}' made on '{1}' from '{2}', has already been existed. Are you sure to continue?";
    }
}
