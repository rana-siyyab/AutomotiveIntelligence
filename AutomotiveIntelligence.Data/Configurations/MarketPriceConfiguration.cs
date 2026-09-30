using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class MarketPriceConfiguration : IEntityTypeConfiguration<MarketPrice>
{
    public void Configure(EntityTypeBuilder<MarketPrice> builder)
    {
        builder.ToTable("MarketPrices");

        builder.HasKey(x => x.MarketPriceId);

        builder.Property(x => x.MarketPriceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ObservedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ObservationDate)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne(x => x.Variant)
    .WithMany()
    .HasForeignKey(x => x.VariantId)
    .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<VehicleCondition>()
            .WithMany()
            .HasForeignKey(x => x.ConditionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DataSource>()
            .WithMany()
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.VariantId,
            x.CityId,
            x.Year
        });

        builder.HasIndex(x => x.ObservationDate);
        builder.Property(x => x.SourceRecordId)
    .IsRequired(false);

        builder.HasIndex(x => new
        {
            x.SourceId,
            x.SourceRecordId
        });
    }
}