using FamilyHub.Data;
using FamilyHub.Data.Finance;
using FamilyHub.DataAccess.EFCore;
using FamilyHub.Service.Contracts;
using FamilyHub.Service.Display;
using FamilyHub.Service.Responses;
using FamilyHub.Service.Services;
using System;
using System.Threading.Tasks;

namespace FamilyHub.Service.Services
{
    public class FinanceService : ServiceFactory, IFinanceService
    {
        public FinanceService(FamilyHubDbContext dbContext)
            : base(dbContext)
        {
        }

        public FinanceService(IUserInfo userInfo, FamilyHubDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

    }
}
