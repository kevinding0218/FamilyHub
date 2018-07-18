using System;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Common;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Payment;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Logging;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Transactions;
using Microsoft.EntityFrameworkCore;
using FamilyHub.DataAccess.EFCore.MappingConfiguration.Member;

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
            #region Logging
            modelBuilder
                .ApplyConfiguration(new ChangeLogConfiguration())
                .ApplyConfiguration(new ChangeLogExclusionConfiguration())
                .ApplyConfiguration(new EventLogConfiguration());
            #endregion

            #region Common
            modelBuilder
                .ApplyConfiguration(new ContactAddressConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new UserPasswordConfiguration())
                .ApplyConfiguration(new ImageSourceConfiguration());
            #endregion

            #region Member
            modelBuilder
                .ApplyConfiguration(new MemberRelationshipConfiguration())
                .ApplyConfiguration(new MemberContactConfiguration());
            #endregion

            #region Payment
            modelBuilder
                .ApplyConfiguration(new PaymentMethodTypeConfiguration())
                .ApplyConfiguration(new PaymentMethodConfiguration())
                .ApplyConfiguration(new PaymentPayorConfiguration());
            #endregion

            #region Transactions
            modelBuilder
                .ApplyConfiguration(new TransactionCategoryConfiguration())
                .ApplyConfiguration(new TransactionTypeConfiguration())
                .ApplyConfiguration(new TransactionDetailConfiguration())
                .ApplyConfiguration(new TransactionConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
