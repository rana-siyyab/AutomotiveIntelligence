using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IMarketIntelligenceRepository
{
    Task<MarketIntelligenceOverviewDto>
        GetOverviewAsync();

    Task<IReadOnlyList<MarketPriceByMakeDto>>
        GetByMakeAsync();

    Task<IReadOnlyList<MarketPriceByModelDto>>
        GetByModelAsync();

    Task<IReadOnlyList<MarketPriceByCityDto>>
        GetByCityAsync();

    Task<IReadOnlyList<MarketPriceByYearDto>>
        GetByYearAsync();

    Task<IReadOnlyList<MarketPriceTrendDto>>
        GetPriceTrendsAsync(int days);
}