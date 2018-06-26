using System;
using System.Collections.Generic;
using System.Text;
using FamilyHub.Data.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyHub.DataAccess.EFCore.MappingConfiguration.Logging
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            // Mapping for table
            builder.ToTable("EventLog", "Logging");

            // Set key for entity
            builder.HasKey(p => p.EventLogID);

            // Set mapping for columns
            builder.Property(p => p.EventType).HasColumnType("int").IsRequired();
            builder.Property(p => p.Key).HasColumnType("varchar(255)").IsRequired();
            builder.Property(p => p.Message).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.EntryDate).HasColumnType("datetime").IsRequired();
        }
    }
}
