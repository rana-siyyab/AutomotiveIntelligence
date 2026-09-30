using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class OicaProductionRepository
    : IOicaProductionRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public OicaProductionRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OicaProduction>> GetAsync(
        int? fromYear = null,
        int? toYear = null,
        string? countryName = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.OicaProductions
            .AsNoTracking()
            .AsQueryable();

        if (fromYear.HasValue)
        {
            query = query.Where(x =>
                x.Year >= fromYear.Value);
        }

        if (toYear.HasValue)
        {
            query = query.Where(x =>
                x.Year <= toYear.Value);
        }

        if (!string.IsNullOrWhiteSpace(countryName))
        {
            query = query.Where(x =>
                x.CountryName == countryName);
        }

        if (!string.IsNullOrWhiteSpace(vehicleType))
        {
            query = query.Where(x =>
                x.VehicleType == vehicleType);
        }

        return await query
            .OrderBy(x => x.Year)
            .ThenBy(x => x.CountryName)
            .ThenBy(x => x.VehicleType)
            .ToListAsync(cancellationToken);
    }
}