using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Common
{
    public class UserPassword : IEntity
    {
        public UserPassword()
        {

        }

        public UserPassword(Int32? _userPasswordID)
        {
            UserPasswordID = _userPasswordID;
        }

        public UserPassword(String _password, Boolean _active)
        {
            Password = _password;
            Active = _active;

            PasswordCreated = DateTime.Now;
        }

        public Int32? UserPasswordID { get; set; }

        public Boolean Active { get; set; }

        public Boolean? IsTemporary { get; set; }

        public String Password { get; set; }

        public DateTime PasswordCreated { get; set; }

        public Int32 UserID { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual User UserFk { get; set; }
    }
}
