using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class VehicleConditionSeedConfiguration
    : IEntityTypeConfiguration<VehicleCondition>
{
    public void Configure(EntityTypeBuilder<VehicleCondition> builder)
    {
        builder.HasData(
            new VehicleCondition
            {
                ConditionId = 1,
                Name = "Excellent",
                Description = "Very well maintained vehicle with minimal signs of use.",
                Score = 95,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new VehicleCondition
            {
                ConditionId = 2,
                Name = "Good",
                Description = "Well maintained vehicle with normal signs of use.",
                Score = 85,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new VehicleCondition
            {
                ConditionId = 3,
                Name = "Average",
                Description = "Vehicle with normal wear and some maintenance requirements.",
                Score = 70,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new VehicleCondition
            {
                ConditionId = 4,
                Name = "Fair",
                Description = "Vehicle with noticeable wear and maintenance requirements.",
                Score = 55,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            },
            new VehicleCondition
            {
                ConditionId = 5,
                Name = "Poor",
                Description = "Vehicle requiring significant maintenance or repairs.",
                Score = 35,
                IsActive = true,
                CreatedDate = new DateTime(2026, 1, 1)
            }
        );
    }
}