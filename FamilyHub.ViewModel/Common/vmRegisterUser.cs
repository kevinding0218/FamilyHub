using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyHub.ViewModel
{
    public class vmRegisterUserRequest
    {
        public String Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }

    public class vmRegisterUserResponse
    {
        public int UserID { get; set; }
        public String Email { get; set; }
    }
}
