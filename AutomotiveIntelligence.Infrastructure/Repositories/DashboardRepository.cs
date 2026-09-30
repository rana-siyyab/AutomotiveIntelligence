using AutomotiveIntelligence.Application.DTOs.Dashboard;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public DashboardRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<MarketOverviewDto> GetMarketOverviewAsync()
    {
        var totalMarketRecords =
            await _context.MarketPrices.CountAsync();

        var totalValuations =
            await _context.Valuations.CountAsync();

        var averageEstimatedPrice =
            await _context.Valuations
                .Select(x => (decimal?)x.EstimatedPrice)
                .AverageAsync() ?? 0;

        var latestObservationDate =
            await _context.MarketPrices
                .Select(x => (DateTime?)x.ObservationDate)
                .MaxAsync();

        var latestAverageMarketPrice = 0m;

        if (latestObservationDate.HasValue)
        {
            latestAverageMarketPrice =
                await _context.MarketPrices
                    .Where(x =>
                        x.ObservationDate ==
                        latestObservationDate.Value)
                    .Select(x => (decimal?)x.ObservedPrice)
                    .AverageAsync() ?? 0;
        }

        return new MarketOverviewDto
        {
            TotalMarketRecords = totalMarketRecords,
            TotalValuations = totalValuations,
            AverageEstimatedPrice = averageEstimatedPrice,
            LatestAverageMarketPrice = latestAverageMarketPrice,
            LastMarketObservationDate = latestObservationDate
        };
    }

    public async Task<IReadOnlyList<PriceTrendDto>>
        GetPriceTrendsAsync(int days = 30)
    {
        if (days < 1)
        {
            days = 30;
        }

        var fromDate = DateTime.UtcNow.Date.AddDays(-days);

        return await _context.MarketPrices
            .Where(x => x.ObservationDate >= fromDate)
            .GroupBy(x => x.ObservationDate.Date)
            .Select(group => new PriceTrendDto
            {
                Date = group.Key,
                AveragePrice = group
                    .Average(x => x.ObservedPrice)
            })
            .OrderBy(x => x.Date)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<PopularModelDto>>
    GetPopularModelsAsync(int count = 10)
    {
        if (count < 1)
        {
            count = 10;
        }

        return await _context.MarketPrices
            .Join(
                _context.Variants,
                market => market.VariantId,
                variant => variant.VariantId,
                (market, variant) => new
                {
                    market,
                    variant
                })
            .Join(
                _context.Models,
                item => item.variant.ModelId,
                model => model.ModelId,
                (item, model) => new
                {
                    ModelId = model.ModelId,
                    ModelName = model.Name,
                    Price = item.market.ObservedPrice
                })
            .GroupBy(x => new
            {
                x.ModelId,
                x.ModelName
            })
            .Select(group => new PopularModelDto
            {
                ModelId = group.Key.ModelId,
                ModelName = group.Key.ModelName,
                RecordCount = group.Count(),
                AveragePrice = group.Average(x => x.Price)
            })
            .OrderByDescending(x => x.RecordCount)
            .Take(count)
            .ToListAsync();
    }
}