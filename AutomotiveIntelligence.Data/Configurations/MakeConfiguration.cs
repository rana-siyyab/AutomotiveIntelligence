using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class MakeConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.ToTable("Makes");

        builder.HasKey(x => x.MakeId);

        builder.Property(x => x.MakeId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Country)
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}