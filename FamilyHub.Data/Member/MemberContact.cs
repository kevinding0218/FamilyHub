using FamilyHub.Data.Common;
using FamilyHub.Data.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Member
{
    public class MemberContact : IAuditableEntity
    {
        public int MemberContactID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public int MemberRelationshipID { get; set; }
        public int ImageSourceID { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual MemberRelationship MemberRelationshipFK { get; set; }
        public virtual ImageSource MemberImageSourceFK { get; set; }

        public virtual PaymentPayor PaymentPayorFK { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
