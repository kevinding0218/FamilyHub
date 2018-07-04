using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Display
{
    public class PaymentMessageDisplay
    {
        public static string PaymentMethodNotFoundMessage
            => "Payment method not found.";
        public static string PaymentMethodAlreadyExistedMessage
            => "Payment method: '{0}', has already been existed, please try another one.";
    }
}
