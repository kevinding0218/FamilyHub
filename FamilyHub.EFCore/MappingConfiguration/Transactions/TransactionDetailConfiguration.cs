using FamilyHub.Data.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Transactions
{
    internal class TransactionDetailConfiguration : IEntityTypeConfiguration<TransactionDetail>
    {
        public void Configure(EntityTypeBuilder<TransactionDetail> builder)
        {
            // Mapping for table
            builder.ToTable("TransactionDetail", "Transactions");

            // Set key for entity
            builder.HasKey(p => p.TransactionDetailID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.TransactionDetailID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.PostedDate).HasColumnType("datetime");
            builder.Property(p => p.TransactionTypeID).HasColumnType("int").IsRequired();
            builder.Property(p => p.TransactionCategoryID).HasColumnType("int").IsRequired();
            builder.Property(p => p.PaymentMethodID).HasColumnType("int").IsRequired();
            builder.Property(p => p.PaymentPayorID).HasColumnType("int").IsRequired();

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(tr => tr.TransactionTypeFk)
                    .WithMany(tt => tt.TransactionDetails)
                    .HasForeignKey(tr => tr.TransactionTypeID);

            builder
                .HasOne(tr => tr.TransactionCategoryFk)
                    .WithMany(tc => tc.TransactionDetails)
                    .HasForeignKey(tr => tr.TransactionCategoryID);

            builder
                .HasOne(tr => tr.PaymentMethodFk)
                    .WithMany(pm => pm.TransactionDetails)
                    .HasForeignKey(tr => tr.PaymentMethodID);

            builder
                .HasOne(tr => tr.PaymentPayorFk)
                    .WithMany(pp => pp.TransactionDetails)
                    .HasForeignKey(tr => tr.PaymentPayorID);
        }
    }
}
