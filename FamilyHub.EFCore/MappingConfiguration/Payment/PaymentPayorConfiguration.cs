using FamilyHub.Data.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Payment
{
    internal class PaymentPayorConfiguration : IEntityTypeConfiguration<PaymentPayor>
    {
        public void Configure(EntityTypeBuilder<PaymentPayor> builder)
        {
            // Mapping for table
            builder.ToTable("PaymentPayor", "Payment");

            // Set key for entity
            builder.HasKey(p => p.PaymentPayorID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.PaymentPayorID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.PaymentPayorName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.PaymentPayorDescription).HasColumnType("nvarchar(250)");
            builder.Property(p => p.Active).HasColumnType("bit").IsRequired();
            builder.Property(p => p.PaymentSplit).HasColumnType("bit").IsRequired();
            builder.Property(p => p.PaymentSplitFactor).HasColumnType("float");
            builder.Property(p => p.PaymentPayorRelationshipID).HasColumnType("int").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnType("int");
            builder.Property(p => p.CreatedOn).HasColumnType("datetime");
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(pp => pp.PaymentPayorRelationshipFk)
                    .WithMany(ppr => ppr.PaymentPayors)
                    .HasForeignKey(pp => pp.PaymentPayorRelationshipID);
        }
    }
}
