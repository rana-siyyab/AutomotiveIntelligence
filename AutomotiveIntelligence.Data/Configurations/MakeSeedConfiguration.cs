using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class MakeSeedConfiguration : IEntityTypeConfiguration<Make>
{
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.HasData(
            new Make
            {
                MakeId = 1,
                Name = "Toyota",
                Country = "Japan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Make
            {
                MakeId = 2,
                Name = "Honda",
                Country = "Japan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Make
            {
                MakeId = 3,
                Name = "Suzuki",
                Country = "Japan",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Make
            {
                MakeId = 4,
                Name = "KIA",
                Country = "South Korea",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new Make
            {
                MakeId = 5,
                Name = "Hyundai",
                Country = "South Korea",
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}