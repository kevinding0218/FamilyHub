using FamilyHub.Data.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Payment
{
    internal class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            // Mapping for table
            builder.ToTable("PaymentMethod", "Payment");

            // Set key for entity
            builder.HasKey(p => p.PaymentMethodID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.PaymentMethodID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.PaymentMethodName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.PaymentMethodDescription).HasColumnType("nvarchar(250)");
            builder.Property(p => p.Active).HasColumnType("bit").IsRequired();
            builder.Property(p => p.PaymentMethodTypeID).HasColumnType("int").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnType("int");
            builder.Property(p => p.CreatedOn).HasColumnType("datetime");
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(pm => pm.PaymentMethodTypeFk)
                .WithMany(pmt => pmt.PaymentMethods)
                .HasForeignKey(pm => pm.PaymentMethodTypeID);
        }
    }
}
