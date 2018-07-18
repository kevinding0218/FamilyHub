using FamilyHub.Data.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Member
{
    internal class MemberRelationshipConfiguration : IEntityTypeConfiguration<MemberRelationship>
    {
        public void Configure(EntityTypeBuilder<MemberRelationship> builder)
        {
            // Mapping for table
            builder.ToTable("MemberRelationship", "Member");

            // Set key for entity
            builder.HasKey(p => p.MemberRelationshipID);

            // Set identity for entity (auto increment)
            builder.Property(p => p.MemberRelationshipID).UseSqlServerIdentityColumn();

            // Set mapping for columns
            builder.Property(p => p.MemberRelationshipName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.MemberRelationshipDescription).HasColumnType("nvarchar(250)");

            // Set concurrency token for entity
            builder.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
