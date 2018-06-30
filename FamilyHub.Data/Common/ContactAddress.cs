using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Common
{
    public class ContactAddress : IAuditableEntity
    {
        public ContactAddress()
        {
        }

        public ContactAddress(Int32 _contactAddressID)
        {
            ContactAddressID = _contactAddressID;
        }

        public Int32 ContactAddressID { get; set; }

        public String Address1 { get; set; }

        public String Address2 { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastUpdatedBy { get; set; }

        public DateTime? LastUpdatedOn { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<User> Users { get; set; } = new Collection<User>();
    }
}
