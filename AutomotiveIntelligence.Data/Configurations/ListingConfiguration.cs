using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.ToTable("Listings");

        builder.HasKey(x => x.ListingId);

        builder.Property(x => x.ListingId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AskingPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.SellerType)
            .HasMaxLength(50);

        builder.Property(x => x.SourceReference)
            .HasMaxLength(250);

        builder.Property(x => x.ListingUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.ListingStatus)
            .HasMaxLength(50);

        builder.Property(x => x.ListingDate)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<DataSource>()
            .WithMany()
            .HasForeignKey(x => x.SourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.VehicleId);

        builder.HasIndex(x => x.CityId);

        builder.HasIndex(x => x.ListingDate);

        builder.HasIndex(x => x.ListingStatus);
    }
}