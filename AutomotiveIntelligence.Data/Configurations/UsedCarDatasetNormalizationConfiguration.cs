using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class UsedCarDatasetNormalizationConfiguration
    : IEntityTypeConfiguration<UsedCarDatasetNormalization>
{
    public void Configure(
        EntityTypeBuilder<UsedCarDatasetNormalization> builder)
    {
        builder.ToTable("UsedCarDatasetNormalizations");

        builder.HasKey(x => x.NormalizationId);

        builder.Property(x => x.NormalizationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.NormalizationStatus)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.NormalizedMakeName)
            .HasMaxLength(200);

        builder.Property(x => x.NormalizedModelName)
            .HasMaxLength(200);

        builder.Property(x => x.NormalizedVariantName)
            .HasMaxLength(300);

        builder.Property(x => x.NormalizedLocationName)
            .HasMaxLength(200);

        builder.Property(x => x.LocationType)
            .HasMaxLength(50);

        builder.Property(x => x.YearStatus)
            .HasMaxLength(50);

        builder.Property(x => x.PriceStatus)
            .HasMaxLength(50);

        builder.Property(x => x.NormalizationNotes)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.NormalizedDate)
            .IsRequired();

        builder.HasOne(x => x.UsedCarDatasetRecord)
            .WithMany()
            .HasForeignKey(x => x.UsedCarDatasetRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UsedCarDatasetRecordId)
            .IsUnique();

        builder.HasIndex(x => new
        {
            x.SourceId,
            x.NormalizationStatus
        });

        builder.HasIndex(x => x.NormalizedMakeName);

        builder.HasIndex(x => x.NormalizedModelName);

        builder.HasIndex(x => x.NormalizedLocationName);

        builder.HasIndex(x => x.YearStatus);

        builder.HasIndex(x => x.PriceStatus);
    }
}