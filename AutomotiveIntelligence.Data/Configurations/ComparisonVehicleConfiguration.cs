using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ComparisonVehicleConfiguration
    : IEntityTypeConfiguration<ComparisonVehicle>
{
    public void Configure(EntityTypeBuilder<ComparisonVehicle> builder)
    {
        builder.ToTable("ComparisonVehicles");

        builder.HasKey(x => new
        {
            x.ComparisonId,
            x.VehicleId
        });

        builder.Property(x => x.DisplayOrder)
            .IsRequired();

        builder.HasOne<Comparison>()
            .WithMany()
            .HasForeignKey(x => x.ComparisonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.ComparisonId,
            x.DisplayOrder
        });
    }
}