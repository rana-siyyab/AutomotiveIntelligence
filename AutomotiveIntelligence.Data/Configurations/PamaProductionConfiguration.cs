using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class PamaProductionConfiguration
    : IEntityTypeConfiguration<PamaProduction>
{
    public void Configure(
        EntityTypeBuilder<PamaProduction> builder)
    {
        builder.ToTable("PamaProductions");

        builder.HasKey(x => x.PamaProductionId);

        builder.Property(x => x.PamaProductionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ManufacturerName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.VehicleType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Year)
            .IsRequired();

        builder.Property(x => x.Month)
            .IsRequired();

        builder.Property(x => x.ProductionUnits)
            .IsRequired();

        builder.Property(x => x.SalesUnits)
            .IsRequired();

        builder.Property(x => x.ObservationDate)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne(x => x.Make)
            .WithMany()
            .HasForeignKey(x => x.MakeId)
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
            x.Year,
            x.Month
        });

        builder.HasIndex(x => new
        {
            x.MakeId,
            x.Year,
            x.Month
        });

        builder.HasIndex(x => x.ObservationDate);
    }
}