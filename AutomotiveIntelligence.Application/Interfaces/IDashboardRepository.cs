using AutomotiveIntelligence.Application.DTOs.Dashboard;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDashboardRepository
{
    Task<MarketOverviewDto> GetMarketOverviewAsync();

    Task<IReadOnlyList<PriceTrendDto>> GetPriceTrendsAsync(
        int days = 30);

    Task<IReadOnlyList<PopularModelDto>> GetPopularModelsAsync(
        int count = 10);
}