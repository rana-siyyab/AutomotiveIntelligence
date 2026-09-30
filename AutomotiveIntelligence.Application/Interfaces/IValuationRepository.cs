using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IValuationRepository
{
    Task<Valuation> AddAsync(
        Valuation valuation,
        IReadOnlyList<ValuationFactor> factors);

    Task<Valuation?> GetByIdAsync(long valuationId);
    Task<IReadOnlyList<ValuationFactor>> GetFactorsByValuationIdAsync(
    long valuationId);
    Task<IReadOnlyList<Valuation>> GetRecentAsync(int pageNumber,int pageSize);
}