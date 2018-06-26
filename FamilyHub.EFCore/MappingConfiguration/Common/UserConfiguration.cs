using System;
using System.Collections.Generic;
using System.Text;
using FamilyHub.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Common
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Mapping for table
            builder.ToTable("Users", "Common");

            // Set key for entity
            builder.HasKey(p => p.UserID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.UserID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.Email).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(p => p.MiddleInitial).HasColumnType("nvarchar(10)");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(p => p.Active).HasColumnType("bit").IsRequired().HasDefaultValue(1);
            builder.Property(p => p.IsCoreUser).HasColumnType("bit");
            builder.Property(p => p.LastLogin).HasColumnType("datetime").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnType("int");
            builder.Property(p => p.CreatedOn).HasColumnType("datetime");
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");

            builder.Property(p => p.Note).HasColumnType("nvarchar(255)");
            builder.Property(p => p.RefreshToken).HasColumnType("nvarchar(300)");
            builder.Property(p => p.ContactAddressID).HasColumnType("int");

            // Add configuration for foreign keys
            builder
                .HasOne(u => u.ContactAddressFk)
                .WithMany(ca => ca.Users)
                .HasForeignKey(u => u.ContactAddressID);
        }
    }
}
