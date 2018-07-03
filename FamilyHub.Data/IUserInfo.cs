using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data
{
    public interface IUserInfo
    {
        int? UID { get; set; }

        string Email { get; set; }

        string[] Roles { get; set; }
    }

    public class UserInfo : IUserInfo
    {
        public UserInfo()
        {
        }

        public string Email { get; set; }

        public int? UID { get; set; }

        public string[] Roles { get; set; }
    }
}
