using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class UsedCarDatasetRecordConfiguration
    : IEntityTypeConfiguration<UsedCarDatasetRecord>
{
    public void Configure(
        EntityTypeBuilder<UsedCarDatasetRecord> builder)
    {
        builder.ToTable("UsedCarDatasetRecords");

        builder.HasKey(x =>
            x.UsedCarDatasetRecordId);

        builder.Property(x =>
            x.UsedCarDatasetRecordId)
            .ValueGeneratedOnAdd();

        builder.Property(x =>
            x.ExternalRecordId)
            .HasMaxLength(200);

        builder.Property(x =>
            x.RawVehicleName)
            .HasMaxLength(500);

        builder.Property(x =>
            x.MakeName)
            .HasMaxLength(200);

        builder.Property(x =>
            x.ModelName)
            .HasMaxLength(200);

        builder.Property(x =>
            x.VariantName)
            .HasMaxLength(300);

        builder.Property(x =>
            x.RawLocationName)
            .HasMaxLength(200);

        builder.Property(x =>
            x.ProvinceName)
            .HasMaxLength(200);

        builder.Property(x =>
            x.CityName)
            .HasMaxLength(200);

        builder.Property(x =>
            x.Transmission)
            .HasMaxLength(100);

        builder.Property(x =>
            x.FuelType)
            .HasMaxLength(100);

        builder.Property(x =>
            x.Color)
            .HasMaxLength(200);

        builder.Property(x =>
            x.AssemblyType)
            .HasMaxLength(100);

        builder.Property(x =>
            x.BodyType)
            .HasMaxLength(100);

        builder.Property(x =>
            x.Features)
            .HasColumnType("nvarchar(max)");

        builder.Property(x =>
            x.SellerName)
            .HasMaxLength(300);

        builder.Property(x =>
            x.AskingPrice)
            .HasPrecision(18, 2);

        builder.Property(x =>
            x.ImageUrl)
            .HasMaxLength(2000);

        builder.Property(x =>
            x.SourceUrl)
            .HasMaxLength(2000);

        builder.Property(x =>
            x.RawDataJson)
            .HasColumnType("nvarchar(max)");

        builder.Property(x =>
            x.ImportedDate)
            .IsRequired();

        builder.HasOne(x =>
            x.Source)
            .WithMany()
            .HasForeignKey(x =>
                x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x =>
            x.Import)
            .WithMany()
            .HasForeignKey(x =>
                x.ImportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x =>
            new
            {
                x.SourceId,
                x.ExternalRecordId
            });

        builder.HasIndex(x =>
            x.ManufacturingYear);

        builder.HasIndex(x =>
            x.MakeName);

        builder.HasIndex(x =>
            x.ModelName);

        builder.HasIndex(x =>
            x.CityName);

        builder.HasIndex(x =>
            x.AskingPrice);
        builder.HasIndex(x => new
        {
            x.SourceId,
            x.UsedCarDatasetRecordId
        });
    }
}