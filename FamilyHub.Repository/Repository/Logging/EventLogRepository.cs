using FamilyHub.Data.Logging;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Repository.Contracts.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHub.Repository.Repository.Logging
{
    public class LoggingRepository : Repository<EventLog>, IEventLogRepository
    {
        public LoggingRepository(FamilyHubDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<EventLog>> GetEventLogs()
            => await GetListAsync();

        public async Task<EventLog> GetEventLog(int eventLogId)
            => await GetSingleOrDefaultAsync(predicate: e => e.EventLogID == eventLogId);
    }
}
