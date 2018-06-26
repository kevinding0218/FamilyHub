using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Logging
{
    public class ChangeLog : IEntity
    {
        public ChangeLog()
        {
        }

        public Int32? ChangeLogID { get; set; }

        public String ClassName { get; set; }

        public String PropertyName { get; set; }

        public String Key { get; set; }

        public String OriginalValue { get; set; }

        public String CurrentValue { get; set; }

        public int UserID { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}
