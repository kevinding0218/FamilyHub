using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.ViewModel
{
    public class vmRefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public vmRefreshTokenRequest()
        {

        }
    }

    public class vmRefreshTokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public vmRefreshTokenResponse()
        {

        }

        public vmRefreshTokenResponse(string token, string refreshToken)
        {
            this.Token = token;
            this.RefreshToken = refreshToken;
        }
    }
}
