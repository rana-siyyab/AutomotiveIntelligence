using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class DataImportConfiguration
    : IEntityTypeConfiguration<DataImport>
{
    public void Configure(
        EntityTypeBuilder<DataImport> builder)
    {
        builder.ToTable("DataImports");

        builder.HasKey(x => x.ImportId);

        builder.Property(x => x.ImportId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FileName)
            .HasMaxLength(500);

        builder.Property(x => x.RecordsProcessed)
            .IsRequired();

        builder.Property(x => x.RecordsInserted)
            .IsRequired();

        builder.Property(x => x.RecordsRejected)
            .IsRequired();

        builder.Property(x => x.RecordsDuplicated)
            .IsRequired();

        builder.Property(x => x.ImportStatus)
            .HasMaxLength(50);

        builder.Property(x => x.ErrorLog)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.StartedDate)
            .IsRequired();

        builder.Property(x => x.CompletedDate);

        builder.HasOne<DataSource>()
            .WithMany()
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.SourceId);

        builder.HasIndex(x => x.StartedDate);
    }
}