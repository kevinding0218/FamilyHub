using System;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Common;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Logging;
using Microsoft.EntityFrameworkCore;

namespace FamilyHub.DataAccess.EFCore
{
    public class FamilyHubDbContext : DbContext
    {
        public FamilyHubDbContext(DbContextOptions<FamilyHubDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations

            modelBuilder
                .ApplyConfiguration(new ChangeLogConfiguration())
                .ApplyConfiguration(new ChangeLogExclusionConfiguration())
                .ApplyConfiguration(new EventLogConfiguration());

            modelBuilder
                .ApplyConfiguration(new ContactAddressConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new UserPasswordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
