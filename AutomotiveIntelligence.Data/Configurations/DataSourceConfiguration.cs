using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class DataSourceConfiguration : IEntityTypeConfiguration<DataSource>
{
    public void Configure(EntityTypeBuilder<DataSource> builder)
    {
        builder.ToTable("DataSources");

        builder.HasKey(x => x.SourceId);

        builder.Property(x => x.SourceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.SourceType)
            .HasMaxLength(50);

        builder.Property(x => x.BaseUrl)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.License)
    .HasMaxLength(200);

        builder.Property(x => x.TermsUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.CommercialUseAllowed)
            .IsRequired();

        builder.Property(x => x.CommercialTrainingAllowed)
            .IsRequired();

        builder.Property(x => x.AttributionRequired)
            .IsRequired();

        builder.Property(x => x.AcquiredDate);
    }
}