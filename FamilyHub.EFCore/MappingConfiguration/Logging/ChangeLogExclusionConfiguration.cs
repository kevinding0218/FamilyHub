using System;
using System.Collections.Generic;
using System.Text;
using FamilyHub.Data.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Logging
{
    internal class ChangeLogExclusionConfiguration : IEntityTypeConfiguration<ChangeLogExclusion>
    {
        public void Configure(EntityTypeBuilder<ChangeLogExclusion> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLogExclusion", "Logging");

            // Set key for entity
            builder.HasKey(p => p.ChangeLogExclusionID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ChangeLogExclusionID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.EntityName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
