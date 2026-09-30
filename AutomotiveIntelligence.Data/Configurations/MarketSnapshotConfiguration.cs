using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class MarketSnapshotConfiguration
    : IEntityTypeConfiguration<MarketSnapshot>
{
    public void Configure(EntityTypeBuilder<MarketSnapshot> builder)
    {
        builder.ToTable("MarketSnapshots");

        builder.HasKey(x => x.SnapshotId);

        builder.Property(x => x.SnapshotId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AveragePrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MedianPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MinimumPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MaximumPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.DemandScore)
            .HasPrecision(5, 2);

        builder.Property(x => x.ListingCount)
            .IsRequired();

        builder.Property(x => x.SnapshotDate)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne<Variant>()
            .WithMany()
            .HasForeignKey(x => x.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.VariantId,
            x.CityId,
            x.SnapshotDate
        })
        .IsUnique();
    }
}