using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class CountrySeedConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(
            new Country
            {
                CountryId = 1,
                Name = "Pakistan",
                Code = "PK",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}