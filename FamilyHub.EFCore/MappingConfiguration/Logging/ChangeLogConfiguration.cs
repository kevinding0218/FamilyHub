using FamilyHub.Data.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Logging
{
    public class ChangeLogConfiguration : IEntityTypeConfiguration<ChangeLog>
    {
        public void Configure(EntityTypeBuilder<ChangeLog> builder)
        {
            // Mapping for table
            builder.ToTable("ChangeLog", "Logging");

            // Set key for entity
            builder.HasKey(p => p.ChangeLogID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ChangeLogID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.ClassName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.PropertyName).HasColumnType("varchar(128)").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.OriginalValue).HasColumnType("varchar(max)");
            builder.Property(p => p.CurrentValue).HasColumnType("varchar(max)");
            builder.Property(p => p.UserID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ChangeDate).HasColumnType("varchar(128)").IsRequired();
        }
    }
}
