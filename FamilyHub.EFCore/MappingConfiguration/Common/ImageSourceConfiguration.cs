using FamilyHub.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Common
{
    internal class ImageSourceConfiguration : IEntityTypeConfiguration<ImageSource>
    {
        public void Configure(EntityTypeBuilder<ImageSource> builder)
        {
            // Mapping for table
            builder.ToTable("ImageSource", "Common");

            // Set key for entity
            builder.HasKey(p => p.ImageSourceID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ImageSourceID).UseSqlServerIdentityColumn();
            builder.Property(p => p.Source)
                .HasColumnName("ImageSource")
                .HasColumnType("nvarchar(150)");
            builder.Property(p => p.UseClass).HasColumnType("bit").IsRequired();
            builder.Property(p => p.IconClass).HasColumnType("nvarchar(30)");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
