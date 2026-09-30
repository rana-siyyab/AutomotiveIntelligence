using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class PriceAlertConfiguration
    : IEntityTypeConfiguration<PriceAlert>
{
    public void Configure(EntityTypeBuilder<PriceAlert> builder)
    {
        builder.ToTable("PriceAlerts");

        builder.HasKey(x => x.PriceAlertId);

        builder.Property(x => x.PriceAlertId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.TargetPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.LastTriggeredDate);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Variant>()
            .WithMany()
            .HasForeignKey(x => x.VariantId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.VariantId
        });

        builder.HasIndex(x => x.IsActive);
    }
}