using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class MarketIntelligenceRepository
    : IMarketIntelligenceRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public MarketIntelligenceRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }


    public async Task<MarketIntelligenceOverviewDto>
        GetOverviewAsync()
    {
        var query = _context.MarketPrices
            .AsNoTracking()
            .Where(x => x.ObservationDate != default);


        var totalRecords =
            await query.CountAsync();


        if (totalRecords == 0)
        {
            return new MarketIntelligenceOverviewDto();
        }


        var averagePrice =
            await query
                .AverageAsync(x => x.ObservedPrice);


        var minimumPrice =
            await query
                .MinAsync(x => x.ObservedPrice);


        var maximumPrice =
            await query
                .MaxAsync(x => x.ObservedPrice);


        var totalMakes =
            await (
                from marketPrice in _context.MarketPrices

                join variant in _context.Variants
                    on marketPrice.VariantId equals variant.VariantId

                join model in _context.Models
                    on variant.ModelId equals model.ModelId

                where marketPrice.ObservationDate != default

                select model.MakeId
            )
            .Distinct()
            .CountAsync();


        var totalModels =
            await query
                .Select(x => x.Variant.ModelId)
                .Distinct()
                .CountAsync();


        var totalCities =
            await query
                .Select(x => x.CityId)
                .Distinct()
                .CountAsync();


        var latestObservationDate =
            await query
                .MaxAsync(x => x.ObservationDate);


        var latestAveragePrice =
            await query
                .Where(x =>
                    x.ObservationDate == latestObservationDate)
                .AverageAsync(x => x.ObservedPrice);


        return new MarketIntelligenceOverviewDto
        {
            TotalMarketRecords = totalRecords,

            TotalMakes = totalMakes,

            TotalModels = totalModels,

            TotalCities = totalCities,

            AverageMarketPrice = averagePrice,

            MinimumMarketPrice = minimumPrice,

            MaximumMarketPrice = maximumPrice,

            LatestAverageMarketPrice =
                latestAveragePrice,

            LatestObservationDate =
                latestObservationDate
        };
    }


    public async Task<IReadOnlyList<MarketPriceByMakeDto>>
        GetByMakeAsync()
    {
        var result =
            await
            (
                from marketPrice in _context.MarketPrices.AsNoTracking()

                join variant in _context.Variants
                    on marketPrice.VariantId
                    equals variant.VariantId

                join model in _context.Models
                    on variant.ModelId
                    equals model.ModelId

                join make in _context.Makes
                    on model.MakeId
                    equals make.MakeId

                where marketPrice.ObservationDate != default

                group marketPrice by new
                {
                    MakeId = make.MakeId,
                    MakeName = make.Name
                }
                into grouped

                select new MarketPriceByMakeDto
                {
                    MakeId = grouped.Key.MakeId,

                    MakeName = grouped.Key.MakeName,

                    RecordCount = grouped.Count(),

                    AveragePrice =
                        grouped.Average(
                            x => x.ObservedPrice),

                    MinimumPrice =
                        grouped.Min(
                            x => x.ObservedPrice),

                    MaximumPrice =
                        grouped.Max(
                            x => x.ObservedPrice)
                }
            )
            .OrderByDescending(x => x.RecordCount)
            .ThenBy(x => x.MakeName)
            .ToListAsync();


        return result;
    }


    public async Task<IReadOnlyList<MarketPriceByModelDto>>
        GetByModelAsync()
    {
        var result =
            await
            (
                from marketPrice in _context.MarketPrices.AsNoTracking()

                join variant in _context.Variants
                    on marketPrice.VariantId
                    equals variant.VariantId

                join model in _context.Models
                    on variant.ModelId
                    equals model.ModelId

                join make in _context.Makes
                    on model.MakeId
                    equals make.MakeId

                where marketPrice.ObservationDate != default

                group marketPrice by new
                {
                    ModelId = model.ModelId,

                    ModelName = model.Name,

                    MakeId = make.MakeId,

                    MakeName = make.Name
                }
                into grouped

                select new MarketPriceByModelDto
                {
                    ModelId =
                        grouped.Key.ModelId,

                    ModelName =
                        grouped.Key.ModelName,

                    MakeId =
                        grouped.Key.MakeId,

                    MakeName =
                        grouped.Key.MakeName,

                    RecordCount =
                        grouped.Count(),

                    AveragePrice =
                        grouped.Average(
                            x => x.ObservedPrice),

                    MinimumPrice =
                        grouped.Min(
                            x => x.ObservedPrice),

                    MaximumPrice =
                        grouped.Max(
                            x => x.ObservedPrice)
                }
            )
            .OrderByDescending(x => x.RecordCount)
            .ThenBy(x => x.MakeName)
            .ThenBy(x => x.ModelName)
            .ToListAsync();


        return result;
    }


    public async Task<IReadOnlyList<MarketPriceByCityDto>>
        GetByCityAsync()
    {
        var result =
            await
            (
                from marketPrice in _context.MarketPrices.AsNoTracking()

                join city in _context.Cities
                    on marketPrice.CityId
                    equals city.CityId

                where marketPrice.ObservationDate != default

                group marketPrice by new
                {
                    CityId = city.CityId,

                    CityName = city.Name
                }
                into grouped

                select new MarketPriceByCityDto
                {
                    CityId =
                        grouped.Key.CityId,

                    CityName =
                        grouped.Key.CityName,

                    RecordCount =
                        grouped.Count(),

                    AveragePrice =
                        grouped.Average(
                            x => x.ObservedPrice),

                    MinimumPrice =
                        grouped.Min(
                            x => x.ObservedPrice),

                    MaximumPrice =
                        grouped.Max(
                            x => x.ObservedPrice)
                }
            )
            .OrderByDescending(x => x.RecordCount)
            .ThenBy(x => x.CityName)
            .ToListAsync();


        return result;
    }


    public async Task<IReadOnlyList<MarketPriceByYearDto>>
        GetByYearAsync()
    {
        return await _context.MarketPrices
            .AsNoTracking()
            .Where(x => x.ObservationDate != default)
            .GroupBy(x => x.Year)
            .Select(group => new MarketPriceByYearDto
            {
                Year = group.Key,

                RecordCount =
                    group.Count(),

                AveragePrice =
                    group.Average(
                        x => x.ObservedPrice),

                MinimumPrice =
                    group.Min(
                        x => x.ObservedPrice),

                MaximumPrice =
                    group.Max(
                        x => x.ObservedPrice)
            })
            .OrderByDescending(x => x.Year)
            .ToListAsync();
    }


    public async Task<IReadOnlyList<MarketPriceTrendDto>>
        GetPriceTrendsAsync(int days)
    {
        if (days < 1)
        {
            days = 30;
        }


        var latestDate =
            await _context.MarketPrices
                .AsNoTracking()
                .Where(x =>
                    x.ObservationDate != default)
                .Select(x =>
                    (DateTime?)x.ObservationDate)
                .MaxAsync();


        if (!latestDate.HasValue)
        {
            return [];
        }


        var startDate =
            latestDate.Value.Date.AddDays(
                -(days - 1));


        return await _context.MarketPrices
            .AsNoTracking()
            .Where(x =>
                x.ObservationDate != default &&
                x.ObservationDate >= startDate)
            .GroupBy(x =>
                x.ObservationDate.Date)
            .Select(group => new MarketPriceTrendDto
            {
                Date = group.Key,

                RecordCount =
                    group.Count(),

                AveragePrice =
                    group.Average(
                        x => x.ObservedPrice),

                MinimumPrice =
                    group.Min(
                        x => x.ObservedPrice),

                MaximumPrice =
                    group.Max(
                        x => x.ObservedPrice)
            })
            .OrderBy(x => x.Date)
            .ToListAsync();
    }
}