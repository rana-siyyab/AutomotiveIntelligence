using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class ModelSeedConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasData(
            // Toyota
            new Model
            {
                ModelId = 1,
                MakeId = 1,
                Name = "Corolla",
                BodyType = "Sedan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 2,
                MakeId = 1,
                Name = "Yaris",
                BodyType = "Sedan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 3,
                MakeId = 1,
                Name = "Fortuner",
                BodyType = "SUV",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Honda
            new Model
            {
                ModelId = 4,
                MakeId = 2,
                Name = "Civic",
                BodyType = "Sedan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 5,
                MakeId = 2,
                Name = "City",
                BodyType = "Sedan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Suzuki
            new Model
            {
                ModelId = 6,
                MakeId = 3,
                Name = "Alto",
                BodyType = "Hatchback",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 7,
                MakeId = 3,
                Name = "Cultus",
                BodyType = "Hatchback",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 8,
                MakeId = 3,
                Name = "Swift",
                BodyType = "Hatchback",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // KIA
            new Model
            {
                ModelId = 9,
                MakeId = 4,
                Name = "Sportage",
                BodyType = "SUV",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 10,
                MakeId = 4,
                Name = "Picanto",
                BodyType = "Hatchback",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },

            // Hyundai
            new Model
            {
                ModelId = 11,
                MakeId = 5,
                Name = "Tucson",
                BodyType = "SUV",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Model
            {
                ModelId = 12,
                MakeId = 5,
                Name = "Elantra",
                BodyType = "Sedan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}