using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.ViewModel
{
    public class vmRefreshTokenRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public vmRefreshTokenRequest()
        {

        }
    }

    public class vmRefreshTokenResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public vmRefreshTokenResponse()
        {

        }

        public vmRefreshTokenResponse(string jwtToken, string refreshToken)
        {
            this.JwtToken = jwtToken;
            this.RefreshToken = refreshToken;
        }
    }
}
