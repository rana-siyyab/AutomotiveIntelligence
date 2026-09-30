using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class UsedCarDatasetVehicleMappingConfiguration
    : IEntityTypeConfiguration<UsedCarDatasetVehicleMapping>
{
    public void Configure(
        EntityTypeBuilder<UsedCarDatasetVehicleMapping> builder)
    {
        builder.ToTable("UsedCarDatasetVehicleMappings");

        builder.HasKey(x => x.MappingId);

        builder.Property(x => x.MappingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MappingStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.MappingMethod)
            .HasMaxLength(100);

        builder.Property(x => x.ConfidenceScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.MappingNotes)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.MappedDate)
            .IsRequired();

        builder.HasOne(x => x.UsedCarDatasetRecord)
            .WithMany()
            .HasForeignKey(x => x.UsedCarDatasetRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Make)
            .WithMany()
            .HasForeignKey(x => x.MakeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey(x => x.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        /*
         * One mapping per source record.
         */
        builder.HasIndex(x => x.UsedCarDatasetRecordId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.SourceId,
            x.MappingStatus
        });

        builder.HasIndex(x => x.MakeId);

        builder.HasIndex(x => x.ModelId);

        builder.HasIndex(x => x.VariantId);

        builder.HasIndex(x => x.ConfidenceScore);
    }
}