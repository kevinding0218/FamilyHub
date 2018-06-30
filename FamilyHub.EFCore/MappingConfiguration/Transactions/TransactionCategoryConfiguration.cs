using FamilyHub.Data.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Transactions
{
    internal class TransactionCategoryConfiguration : IEntityTypeConfiguration<TransactionCategory>
    {
        public void Configure(EntityTypeBuilder<TransactionCategory> builder)
        {
            // Mapping for table
            builder.ToTable("TransactionCategory", "Transactions");

            // Set key for entity
            builder.HasKey(p => p.TransactionCategoryID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.TransactionCategoryID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.TransactionCategoryName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.TransactionCategoryDescription).HasColumnType("nvarchar(250)");
            builder.Property(p => p.IsFixed).HasColumnType("bit").IsRequired();

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
