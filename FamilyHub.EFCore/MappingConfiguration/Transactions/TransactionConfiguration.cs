using FamilyHub.Data.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Transactions
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            // Mapping for table
            builder.ToTable("Transaction", "Transactions");

            // Set key for entity
            builder.HasKey(p => p.TransactionID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.TransactionID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.TransactionDate).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.TransactionDescription).HasColumnType("nvarchar(250)").IsRequired();
            builder.Property(p => p.Amount).HasColumnType("float").IsRequired();
            builder.Property(p => p.TransactionDetailID).HasColumnType("int").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnType("int");
            builder.Property(p => p.CreatedOn).HasColumnType("datetime");
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for one to one
            builder
                .HasOne(tr => tr.TransactionDetailFk)
                    .WithOne(td => td.TransactionFk)
                    .HasForeignKey<Transaction>(tr => tr.TransactionDetailID);
        }
    }
}
