using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.AuthService.Helper
{
    public static class Constants
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

        public static class JwtPolicys
        {
            public const string RoleAdminRequired = "RoleAdminRequired";
        }

        public static class JwtTokenResult
        {
            public const string TokenExpired = "Token-Expired";
            public const string TokenInvalid = "Token-Invalid";
            public const string TokenForbidden = "Token-Forbidden";
        }
    }
}
