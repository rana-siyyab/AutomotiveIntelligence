using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VehicleConditionConfiguration
    : IEntityTypeConfiguration<VehicleCondition>
{
    public void Configure(EntityTypeBuilder<VehicleCondition> builder)
    {
        builder.ToTable("VehicleConditions");

        builder.HasKey(x => x.ConditionId);

        builder.Property(x => x.ConditionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Score)
            .HasPrecision(5, 2);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}