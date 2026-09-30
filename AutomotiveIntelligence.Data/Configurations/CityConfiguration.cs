using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(x => x.CityId);

        builder.Property(x => x.CityId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => new
        {
            x.ProvinceId,
            x.Name
        })
        .IsUnique();

        builder.HasOne<Province>()
            .WithMany()
            .HasForeignKey(x => x.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}