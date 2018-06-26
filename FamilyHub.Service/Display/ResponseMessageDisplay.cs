using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Display
{
    public static class ResponseMessageDisplay
    {
        public static string Success
            => "SUCCESS";
        public static string Warning
            => "Warning";
        public static string Error
            => "Error";
        public static string Valid
            => "Valid";
        public static string Invalid
            => "Invalid";
        public static string NotAuthorized
            => "NotAuthorized";
        public static string IsExisted
            => "Existed";
        public static string NonExisted
            => "NonExisted";
        public static string Duplicate
            => "Duplicate";
    }
}
