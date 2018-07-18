using FamilyHub.Data.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Member
{
    internal class MemberContactConfiguration : IEntityTypeConfiguration<MemberContact>
    {
        public void Configure(EntityTypeBuilder<MemberContact> builder)
        {
            // Mapping for table
            builder.ToTable("MemberContact", "Member");

            // Set key for entity
            builder.HasKey(p => p.MemberContactID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.MemberContactID).UseSqlServerIdentityColumn();

            // Set mapping for columns            
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(p => p.MiddleInitial).HasColumnType("nvarchar(10)");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(30)").IsRequired();
            builder.Property(p => p.HomePhone).HasColumnType("nvarchar(20)");
            builder.Property(p => p.MobilePhone).HasColumnType("nvarchar(20)");
            builder.Property(p => p.Location).HasColumnType("nvarchar(50)");
            builder.Property(p => p.EmailAddress).HasColumnType("nvarchar(100)");

            builder.Property(p => p.MemberRelationshipID).HasColumnType("int").IsRequired();
            builder.Property(p => p.ImageSourceID).HasColumnType("int");

            builder.Property(p => p.CreatedBy).HasColumnType("int");
            builder.Property(p => p.CreatedOn).HasColumnType("datetime");
            builder.Property(p => p.LastUpdatedBy).HasColumnType("int");
            builder.Property(p => p.LastUpdatedOn).HasColumnType("datetime");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

            // Add configuration for foreign keys
            builder
                .HasOne(u => u.MemberRelationshipFK)
                .WithMany(ca => ca.MemberContacts)
                .HasForeignKey(u => u.MemberRelationshipID);

            builder
                .HasOne(u => u.MemberImageSourceFK)
                .WithMany(ca => ca.MemberContacts)
                .HasForeignKey(u => u.ImageSourceID);
        }
    }
}
