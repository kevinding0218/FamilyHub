using FamilyHub.Data.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Transactions
{
    internal class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            // Mapping for table
            builder.ToTable("TransactionType", "Transactions");

            // Set key for entity
            builder.HasKey(p => p.TransactionTypeID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.TransactionTypeID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.TransactionTypeName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.TransactionTypeDescription).HasColumnType("nvarchar(250)");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
