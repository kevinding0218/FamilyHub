using System;
using System.Collections.Generic;
using System.Text;
using FamilyHub.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Common
{
    public class ContactAddressConfiguration : IEntityTypeConfiguration<ContactAddress>
    {
        public void Configure(EntityTypeBuilder<ContactAddress> builder)
        {
            // Mapping for table
            builder.ToTable("ContactAddress", "Common");

            // Set key for entity
            builder.HasKey(p => p.ContactAddressID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.ContactAddressID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.Address1).HasColumnType("nvarchar(100)");
            builder.Property(p => p.Address2).HasColumnType("nvarchar(100)");
            builder.Property(p => p.City).HasColumnType("nvarchar(30)");
            builder.Property(p => p.State).HasColumnType("nvarchar(5)");
            builder.Property(p => p.ZipCode).HasColumnType("nvarchar(15)");
            builder.Property(p => p.CreatedBy).HasColumnType("int").IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime").IsRequired();
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");
        }
    }
}
