using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models");

        builder.HasKey(x => x.ModelId);

        builder.Property(x => x.ModelId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.BodyType)
            .HasMaxLength(50);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasIndex(x => new
        {
            x.MakeId,
            x.Name
        })
        .IsUnique();

        builder.HasOne<Make>()
            .WithMany()
            .HasForeignKey(x => x.MakeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}