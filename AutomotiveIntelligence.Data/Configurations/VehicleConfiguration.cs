using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.VehicleId);

        builder.Property(x => x.VehicleId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ManufacturingYear)
            .IsRequired();

        builder.Property(x => x.RegistrationYear);

        builder.Property(x => x.Mileage);

        builder.Property(x => x.Color)
            .HasMaxLength(50);

        builder.Property(x => x.OwnerCount);

        builder.Property(x => x.HasAccidentHistory);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasOne<Variant>()
    .WithMany()
    .HasForeignKey(x => x.VariantId)
    .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<VehicleCondition>()
            .WithMany()
            .HasForeignKey(x => x.ConditionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}