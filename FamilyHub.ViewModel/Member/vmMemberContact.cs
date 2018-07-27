using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.ViewModel.Member
{
    public class vmMemberContactDetailRequest
    {
        public int MemberContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string Location { get; set; }
        public string EmailAddress { get; set; }
        public string MemberRelationshipID { get; set; }
    }

    public class vmMemberContactListResponse : vmMemberContactDetailRequest
    {
        public string FullName { get; set; }
        public string ContactPhone { get; set; }

        public DateTime? CreatedOn { get; set; }
        public string MemberRelationshipName { get; set; }
        public string ImageSource { get; set; }
    }

    public class vmMemberContactDetailResponse : vmMemberContactListResponse
    {
    }
}
