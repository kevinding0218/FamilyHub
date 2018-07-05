using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.ViewModel
{
    public class vmLoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class vmAuthorizedUserResponse
    {
        public vmAuthorizedUserResponse()
        {

        }

        public int UserID { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class vmValidateUserResponse : vmAuthorizedUserResponse
    {
        public string Password { get; set; }
    }
}
