using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IMarketPriceRepository
{
    Task<IReadOnlyList<MarketPrice>> GetMarketPricesAsync(
        int? makeId,
        int? modelId,
        int? variantId,
        int? year,
        int? cityId);
}