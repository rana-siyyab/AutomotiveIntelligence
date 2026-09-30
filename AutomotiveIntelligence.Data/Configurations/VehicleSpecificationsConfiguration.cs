using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VehicleSpecificationsConfiguration
    : IEntityTypeConfiguration<VehicleSpecifications>
{
    public void Configure(
        EntityTypeBuilder<VehicleSpecifications> builder)
    {
        builder.ToTable("VehicleSpecifications");

        builder.HasKey(x => x.VehicleSpecificationsId);

        builder.Property(x => x.VehicleSpecificationsId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EngineCapacity);

        builder.Property(x => x.Horsepower);

        builder.Property(x => x.Torque);

        builder.Property(x => x.Transmission)
            .HasMaxLength(50);

        builder.Property(x => x.FuelType)
            .HasMaxLength(50);

        builder.Property(x => x.DriveType)
            .HasMaxLength(50);

        builder.Property(x => x.SeatingCapacity);

        builder.Property(x => x.Length)
            .HasPrecision(10, 2);

        builder.Property(x => x.Width)
            .HasPrecision(10, 2);

        builder.Property(x => x.Height)
            .HasPrecision(10, 2);

        builder.Property(x => x.Wheelbase)
            .HasPrecision(10, 2);

        builder.Property(x => x.GroundClearance)
            .HasPrecision(10, 2);

        builder.Property(x => x.FuelTankCapacity)
            .HasPrecision(10, 2);

        builder.Property(x => x.BootSpace)
            .HasPrecision(10, 2);

        builder.Property(x => x.AirbagCount);

        builder.Property(x => x.HasABS);

        builder.Property(x => x.HasSunroof);

        builder.Property(x => x.HasTractionControl);

        builder.Property(x => x.HasStabilityControl);

        builder.Property(x => x.HasInfotainment);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => x.VariantId)
            .IsUnique();

        builder.HasOne<Variant>()
            .WithOne()
            .HasForeignKey<VehicleSpecifications>(
                x => x.VariantId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}