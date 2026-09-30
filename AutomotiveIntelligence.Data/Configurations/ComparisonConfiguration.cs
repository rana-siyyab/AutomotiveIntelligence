using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ComparisonConfiguration : IEntityTypeConfiguration<Comparison>
{
    public void Configure(EntityTypeBuilder<Comparison> builder)
    {
        builder.ToTable("Comparisons");

        builder.HasKey(x => x.ComparisonId);

        builder.Property(x => x.ComparisonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
    }
}