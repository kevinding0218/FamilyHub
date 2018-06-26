using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.API.ViewModel
{
    public class vmLoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class vmLoginUserResponse
    {
        public vmLoginUserResponse()
        {

        }

        public vmLoginUserResponse(int _userID, string _email, string _jwtToken, string _refreshToken)
        {
            UserID = _userID;
            Email = _email;
            JwtToken = _jwtToken;
            RefreshToken = _refreshToken;
        }

        public int UserID { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
