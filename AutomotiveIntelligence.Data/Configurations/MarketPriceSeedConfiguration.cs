using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutomotiveIntelligence.Data.Configurations;

public class MarketPriceSeedConfiguration : IEntityTypeConfiguration<MarketPrice>
{
    public void Configure(EntityTypeBuilder<MarketPrice> builder)
    {
        builder.HasData(
            new MarketPrice
            {
                MarketPriceId = 1,
                VariantId = 1,
                Year = 2022,
                CityId = 1,
                ConditionId = 2,
                Mileage = 45000,
                ObservedPrice = 7200000,
                SourceId = null,
                ObservationDate = new DateTime(2026, 8, 1),
                CreatedDate = new DateTime(2026, 8, 1)
            },
            new MarketPrice
            {
                MarketPriceId = 2,
                VariantId = 1,
                Year = 2022,
                CityId = 1,
                ConditionId = 2,
                Mileage = 52000,
                ObservedPrice = 7000000,
                SourceId = null,
                ObservationDate = new DateTime(2026, 8, 10),
                CreatedDate = new DateTime(2026, 8, 10)
            },
            new MarketPrice
            {
                MarketPriceId = 3,
                VariantId = 1,
                Year = 2022,
                CityId = 1,
                ConditionId = 1,
                Mileage = 30000,
                ObservedPrice = 7500000,
                SourceId = null,
                ObservationDate = new DateTime(2026, 8, 15),
                CreatedDate = new DateTime(2026, 8, 15)
            },
            new MarketPrice
            {
                MarketPriceId = 4,
                VariantId = 1,
                Year = 2022,
                CityId = 1,
                ConditionId = 3,
                Mileage = 70000,
                ObservedPrice = 6700000,
                SourceId = null,
                ObservationDate = new DateTime(2026, 8, 20),
                CreatedDate = new DateTime(2026, 8, 20)
            },
            new MarketPrice
            {
                MarketPriceId = 5,
                VariantId = 1,
                Year = 2022,
                CityId = 1,
                ConditionId = 2,
                Mileage = 60000,
                ObservedPrice = 6900000,
                SourceId = null,
                ObservationDate = new DateTime(2026, 8, 25),
                CreatedDate = new DateTime(2026, 8, 25)
            }
        );
    }
}