using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        builder.HasKey(x => x.CountryId);

        builder.Property(x => x.CountryId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Code)
            .HasMaxLength(10);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasFilter("[Code] IS NOT NULL");
    }
}