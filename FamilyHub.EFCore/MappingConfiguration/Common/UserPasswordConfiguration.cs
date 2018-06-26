using System;
using System.Collections.Generic;
using System.Text;
using FamilyHub.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Common
{
    public class UserPasswordConfiguration : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(EntityTypeBuilder<UserPassword> builder)
        {
            // Mapping for table
            builder.ToTable("UserPassword", "Common");

            // Set key for entity
            builder.HasKey(p => p.UserPasswordID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.UserPasswordID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.Active).HasColumnType("bit").IsRequired();
            builder.Property(p => p.IsTemporary).HasColumnType("bit").IsRequired();
            builder.Property(p => p.Password).HasColumnType("nvarchar(25)").IsRequired();
            builder.Property(p => p.PasswordCreated).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.UserID).HasColumnType("int").IsRequired();

            // Add configuration for foreign keys
            builder
                .HasOne(u => u.UserFk)
                .WithMany(up => up.UserPasswords)
                .HasForeignKey(u => u.UserID);
        }
    }
}
