using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Service.Exceptions
{
    public class FamilyHubException : Exception
    {
        public FamilyHubException()
            : base()
        {
        }

        public FamilyHubException(String message)
            : base(message)
        {
        }

        public FamilyHubException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
