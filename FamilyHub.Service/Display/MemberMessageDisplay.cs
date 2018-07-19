using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Display
{
    public class MemberMessageDisplay
    {
        public static string MemberContactNotFoundMessage
            => "Member not found.";
        public static string MemberContactAlreadyExistedMessage
            => "Member: '{0}', has already been existed, please try another one.";
    }
}
