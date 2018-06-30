using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Common
{
    public class User : IAuditableEntity
    {
        public User()
        {

        }

        public User(Int32 _userID)
        {
            UserID = _userID;
        }

        public User(String _email)
        {
            Email = _email;
        }

        public Int32 UserID { get; set; }

        public String Email { get; set; }

        public String FirstName { get; set; }

        public String MiddleInitial { get; set; }

        public String LastName { get; set; }

        public Boolean Active { get; set; }

        public Boolean? IsCoreUser { get; set; }

        public DateTime LastLogin { get; set; }

        public String Note { get; set; }

        public String RefreshToken { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public Int32? ContactAddressID { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual ContactAddress ContactAddressFk { get; set; }

        public virtual Collection<UserPassword> UserPasswords { get; set; } = new Collection<UserPassword>();
    }
}
