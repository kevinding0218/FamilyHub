using FamilyHub.Data.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Finance
{
    internal class PaymentPayorRelationshipConfiguration : IEntityTypeConfiguration<PaymentPayorRelationship>
    {
        public void Configure(EntityTypeBuilder<PaymentPayorRelationship> builder)
        {
            // Mapping for table
            builder.ToTable("PaymentPayorRelationship", "Finance");

            // Set key for entity
            builder.HasKey(p => p.PaymentPayorRelationshipID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.PaymentPayorRelationshipID).UseSqlServerIdentityColumn();
            // Set mapping for columns
            builder.Property(p => p.PaymentPayorRelationshipName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.PaymentPayorRelationshipDescription).HasColumnType("nvarchar(250)");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
