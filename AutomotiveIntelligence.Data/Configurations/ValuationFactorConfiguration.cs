using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ValuationFactorConfiguration
    : IEntityTypeConfiguration<ValuationFactor>
{
    public void Configure(
        EntityTypeBuilder<ValuationFactor> builder)
    {
        builder.ToTable("ValuationFactors");

        builder.HasKey(x => x.ValuationFactorId);

        builder.Property(x => x.ValuationFactorId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FactorType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.FactorName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.AdjustmentValue)
            .HasPrecision(18, 2);

        builder.Property(x => x.AdjustmentPercentage)
            .HasPrecision(7, 4);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne<Valuation>()
            .WithMany()
            .HasForeignKey(x => x.ValuationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ValuationId);
    }
}