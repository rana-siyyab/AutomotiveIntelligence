using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ValuationConfiguration : IEntityTypeConfiguration<Valuation>
{
    public void Configure(EntityTypeBuilder<Valuation> builder)
    {
        builder.ToTable("Valuations");

        builder.HasKey(x => x.ValuationId);

        builder.Property(x => x.ValuationId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AskingPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.EstimatedPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MinimumPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.MaximumPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ConfidenceScore)
            .HasPrecision(5, 2)
            .IsRequired();
        builder.Property(x => x.RecommendedBuyingPrice)
    .HasPrecision(18, 2);

        builder.Property(x => x.RecommendedNegotiationPrice)
            .HasPrecision(18, 2);
        builder.Property(x => x.ValuationMethod)
            .HasMaxLength(100);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne<Variant>()
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
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.VariantId);

        builder.HasIndex(x => x.CreatedDate);
    }
}