using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class OicaProductionConfiguration
    : IEntityTypeConfiguration<OicaProduction>
{
    public void Configure(
        EntityTypeBuilder<OicaProduction> builder)
    {
        builder.ToTable("OicaProductions");

        builder.HasKey(x => x.OicaProductionId);

        builder.Property(x => x.OicaProductionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CountryName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.VehicleType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.ProductionUnits)
            .IsRequired();

        builder.Property(x => x.ObservationDate)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DataSource>()
            .WithMany()
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DataImport>()
            .WithMany()
            .HasForeignKey(x => x.ImportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.SourceId,
            x.CountryName,
            x.VehicleType,
            x.Year
        });

        builder.HasIndex(x => new
        {
            x.CountryName,
            x.Year
        });

        builder.HasIndex(x => x.ObservationDate);
    }
}