using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.AuthService.Helper
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
                public const string InternalUser = "internal";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string ApiInternalAccess = "api_internal_access";
            }
        }
    }
}
