using FamilyHub.Data.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Finance
{
    internal class PaymentMethodTypeConfiguration : IEntityTypeConfiguration<PaymentMethodType>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodType> builder)
        {
            // Mapping for table
            builder.ToTable("PaymentMethodType", "Finance");

            // Set key for entity
            builder.HasKey(p => p.PaymentMethodTypeID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.PaymentMethodTypeID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.PaymentMethodTypeName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.PaymentMethodTypeDescription).HasColumnType("nvarchar(250)");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
