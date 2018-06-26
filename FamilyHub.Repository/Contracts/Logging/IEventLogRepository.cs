using FamilyHub.Data.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Contracts.Logging
{
    public interface IEventLogRepository : IRepository<EventLog>
    {
        Task<IEnumerable<EventLog>> GetEventLogs();

        Task<EventLog> GetEventLog(int eventLogId);
    }
}
