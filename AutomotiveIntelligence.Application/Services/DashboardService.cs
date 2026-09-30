using AutomotiveIntelligence.Application.DTOs.Dashboard;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class DashboardService
{
    private readonly IDashboardRepository _repository;

    public DashboardService(IDashboardRepository repository)
    {
        _repository = repository;
    }

    public Task<MarketOverviewDto> GetMarketOverviewAsync()
    {
        return _repository.GetMarketOverviewAsync();
    }

    public Task<IReadOnlyList<PriceTrendDto>> GetPriceTrendsAsync(
        int days = 30)
    {
        return _repository.GetPriceTrendsAsync(days);
    }

    public Task<IReadOnlyList<PopularModelDto>> GetPopularModelsAsync(
        int count = 10)
    {
        return _repository.GetPopularModelsAsync(count);
    }
}