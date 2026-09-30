using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ProvinceSeedConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasData(
            new Province
            {
                ProvinceId = 1,
                CountryId = 1,
                Name = "Punjab",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Province
            {
                ProvinceId = 2,
                CountryId = 1,
                Name = "Sindh",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Province
            {
                ProvinceId = 3,
                CountryId = 1,
                Name = "Khyber Pakhtunkhwa",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Province
            {
                ProvinceId = 4,
                CountryId = 1,
                Name = "Balochistan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Province
            {
                ProvinceId = 5,
                CountryId = 1,
                Name = "Islamabad Capital Territory",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}