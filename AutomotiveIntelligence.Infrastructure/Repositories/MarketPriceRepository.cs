using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class MarketPriceRepository : IMarketPriceRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public MarketPriceRepository(AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<MarketPrice>> GetMarketPricesAsync(
        int? makeId,
        int? modelId,
        int? variantId,
        int? year,
        int? cityId)
    {
        var query = _context.MarketPrices
            .AsNoTracking()
            .Include(x => x.Variant)
            .ThenInclude(x => x.Model)
            .Where(x => x.ObservationDate != default);

        if (makeId.HasValue)
        {
            query = query.Where(x =>
                x.Variant.Model.MakeId == makeId.Value);
        }

        if (modelId.HasValue)
        {
            query = query.Where(x =>
                x.Variant.ModelId == modelId.Value);
        }

        if (variantId.HasValue)
        {
            query = query.Where(x =>
                x.VariantId == variantId.Value);
        }

        if (year.HasValue)
        {
            query = query.Where(x =>
                x.Year == year.Value);
        }

        if (cityId.HasValue)
        {
            query = query.Where(x =>
                x.CityId == cityId.Value);
        }

        return await query
            .OrderByDescending(x => x.ObservationDate)
            .ToListAsync();
    }
}