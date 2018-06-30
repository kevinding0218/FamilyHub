using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.Data.Logging
{
    public class EventLog : IEntity
    {
        public EventLog()
        {
        }

        public Int32? EventLogID { get; set; }

        public Int32? EventType { get; set; }

        public String Key { get; set; }

        public String Message { get; set; }

        public DateTime? EntryDate { get; set; }

        public Byte[] Timestamp { get; set; }
    }
}
