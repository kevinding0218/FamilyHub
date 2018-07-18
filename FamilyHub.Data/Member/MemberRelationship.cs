using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Member
{
    public class MemberRelationship
    {
        public int MemberRelationshipID { get; set; }
        public string MemberRelationshipName { get; set; }
        public string MemberRelationshipDescription { get; set; }
        public Byte[] Timestamp { get; set; }

        public virtual Collection<MemberContact> MemberContacts { get; set; } = new Collection<MemberContact>();
    }
}
