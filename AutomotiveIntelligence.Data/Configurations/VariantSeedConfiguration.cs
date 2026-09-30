using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VariantSeedConfiguration : IEntityTypeConfiguration<Variant>
{
    public void Configure(EntityTypeBuilder<Variant> builder)
    {
        builder.HasData(
            // Toyota Corolla
            new Variant
            {
                VariantId = 1,
                ModelId = 1,
                Name = "Altis X",
                EngineCapacity = 1800,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Variant
            {
                VariantId = 2,
                ModelId = 1,
                Name = "Altis Grande",
                EngineCapacity = 1800,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Toyota Yaris
            new Variant
            {
                VariantId = 3,
                ModelId = 2,
                Name = "GLI",
                EngineCapacity = 1300,
                Transmission = "Manual",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Variant
            {
                VariantId = 4,
                ModelId = 2,
                Name = "ATIV",
                EngineCapacity = 1300,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Toyota Fortuner
            new Variant
            {
                VariantId = 5,
                ModelId = 3,
                Name = "2.7 V",
                EngineCapacity = 2700,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "4WD",
                SeatingCapacity = 7,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Honda Civic
            new Variant
            {
                VariantId = 6,
                ModelId = 4,
                Name = "1.5 Turbo",
                EngineCapacity = 1500,
                Transmission = "CVT",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Honda City
            new Variant
            {
                VariantId = 7,
                ModelId = 5,
                Name = "1.2",
                EngineCapacity = 1200,
                Transmission = "Manual",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Suzuki Alto
            new Variant
            {
                VariantId = 8,
                ModelId = 6,
                Name = "VXR",
                EngineCapacity = 660,
                Transmission = "Manual",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 4,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Variant
            {
                VariantId = 9,
                ModelId = 6,
                Name = "VXL AGS",
                EngineCapacity = 660,
                Transmission = "AMT",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 4,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Suzuki Cultus
            new Variant
            {
                VariantId = 10,
                ModelId = 7,
                Name = "VXL",
                EngineCapacity = 998,
                Transmission = "Manual",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Suzuki Swift
            new Variant
            {
                VariantId = 11,
                ModelId = 8,
                Name = "GL",
                EngineCapacity = 1200,
                Transmission = "Manual",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // KIA Sportage
            new Variant
            {
                VariantId = 12,
                ModelId = 9,
                Name = "FWD",
                EngineCapacity = 2000,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // KIA Picanto
            new Variant
            {
                VariantId = 13,
                ModelId = 10,
                Name = "1.0",
                EngineCapacity = 1000,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Hyundai Tucson
            new Variant
            {
                VariantId = 14,
                ModelId = 11,
                Name = "FWD",
                EngineCapacity = 2000,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Hyundai Elantra
            new Variant
            {
                VariantId = 15,
                ModelId = 12,
                Name = "GL",
                EngineCapacity = 2000,
                Transmission = "Automatic",
                FuelType = "Petrol",
                DriveType = "FWD",
                SeatingCapacity = 5,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}