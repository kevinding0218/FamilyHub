using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Logging
{
    public class ChangeLogExclusion : IEntity
    {
        public ChangeLogExclusion()
        {
        }

        public int? ChangeLogExclusionID { get; set; }

        public string EntityName { get; set; }

        public string PropertyName { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
