using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Display
{
    public class CommonMessageDisplays
    {
        public static string ExistedUserExceptionMessage
            => "User with Email: '{0}', has already been registered.";
        public static string FailedAuthenticationExceptionMessage
            => "User email or password does not match.";
        public static string UserNotFoundExceptionMessage
            => "User with Email: '{0}' not found.";
    }
}
