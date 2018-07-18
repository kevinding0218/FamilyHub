using FamilyHub.Data.Member;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FamilyHub.Data.Common
{
    public class ImageSource
    {
        public int ImageSourceID { get; set; }
        public string Source { get; set; }
        public bool UseClass { get; set; }
        public string IconClass { get; set; }

        public Byte[] Timestamp { get; set; }

        public virtual Collection<MemberContact> MemberContacts { get; set; } = new Collection<MemberContact>();
    }
}
