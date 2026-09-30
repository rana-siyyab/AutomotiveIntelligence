using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class ValuationRepository : IValuationRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public ValuationRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<Valuation> AddAsync(
        Valuation valuation,
        IReadOnlyList<ValuationFactor> factors)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            _context.Valuations.Add(valuation);

            await _context.SaveChangesAsync();

            foreach (var factor in factors)
            {
                factor.ValuationId = valuation.ValuationId;
            }

            _context.ValuationFactors.AddRange(factors);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return valuation;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task<Valuation?> GetByIdAsync(long valuationId)
    {
        return await _context.Valuations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ValuationId == valuationId);
    }
    public async Task<IReadOnlyList<ValuationFactor>>
    GetFactorsByValuationIdAsync(long valuationId)
    {
        return await _context.ValuationFactors
            .AsNoTracking()
            .Where(x => x.ValuationId == valuationId)
            .OrderBy(x => x.ValuationFactorId)
            .ToListAsync();
    }
    public async Task<IReadOnlyList<Valuation>> GetRecentAsync(
    int pageNumber,
    int pageSize)
    {
        return await _context.Valuations
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}