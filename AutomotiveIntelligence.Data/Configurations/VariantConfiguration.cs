using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VariantConfiguration : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
        builder.ToTable("Variants");

        builder.HasKey(x => x.VariantId);

        builder.Property(x => x.VariantId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.EngineCapacity);

        builder.Property(x => x.Transmission)
            .HasMaxLength(50);

        builder.Property(x => x.FuelType)
            .HasMaxLength(50);

        builder.Property(x => x.DriveType)
            .HasMaxLength(50);

        builder.Property(x => x.SeatingCapacity);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => new
        {
            x.ModelId,
            x.Name
        })
        .IsUnique();

        builder.HasOne(x => x.Model)
            .WithMany()
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}