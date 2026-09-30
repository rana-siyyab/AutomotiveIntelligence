using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class CitySeedConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasData(
            // Punjab
            new City
            {
                CityId = 1,
                ProvinceId = 1,
                Name = "Lahore",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 2,
                ProvinceId = 1,
                Name = "Rawalpindi",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 3,
                ProvinceId = 1,
                Name = "Faisalabad",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 4,
                ProvinceId = 1,
                Name = "Multan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 5,
                ProvinceId = 1,
                Name = "Gujranwala",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Sindh
            new City
            {
                CityId = 6,
                ProvinceId = 2,
                Name = "Karachi",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 7,
                ProvinceId = 2,
                Name = "Hyderabad",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Khyber Pakhtunkhwa
            new City
            {
                CityId = 8,
                ProvinceId = 3,
                Name = "Peshawar",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new City
            {
                CityId = 9,
                ProvinceId = 3,
                Name = "Abbottabad",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Balochistan
            new City
            {
                CityId = 10,
                ProvinceId = 4,
                Name = "Quetta",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Islamabad Capital Territory
            new City
            {
                CityId = 11,
                ProvinceId = 5,
                Name = "Islamabad",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}