using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class MarketIntelligenceService
    : IMarketIntelligenceService
{
    private readonly IMarketIntelligenceRepository
        _repository;

    public MarketIntelligenceService(
        IMarketIntelligenceRepository repository)
    {
        _repository = repository;
    }


    public Task<MarketIntelligenceOverviewDto>
        GetOverviewAsync()
    {
        return _repository.GetOverviewAsync();
    }


    public Task<IReadOnlyList<MarketPriceByMakeDto>>
        GetByMakeAsync()
    {
        return _repository.GetByMakeAsync();
    }


    public Task<IReadOnlyList<MarketPriceByModelDto>>
        GetByModelAsync()
    {
        return _repository.GetByModelAsync();
    }


    public Task<IReadOnlyList<MarketPriceByCityDto>>
        GetByCityAsync()
    {
        return _repository.GetByCityAsync();
    }


    public Task<IReadOnlyList<MarketPriceByYearDto>>
        GetByYearAsync()
    {
        return _repository.GetByYearAsync();
    }


    public Task<IReadOnlyList<MarketPriceTrendDto>>
        GetPriceTrendsAsync(int days)
    {
        return _repository.GetPriceTrendsAsync(days);
    }
}