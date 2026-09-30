using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class PamaProductionRepository
    : IPamaProductionRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public PamaProductionRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PamaProduction>> GetAsync(
        int? year = null,
        int? makeId = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PamaProductions
            .AsNoTracking()
            .AsQueryable();

        if (year.HasValue)
        {
            query = query.Where(x => x.Year == year.Value);
        }

        if (makeId.HasValue)
        {
            query = query.Where(x => x.MakeId == makeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(vehicleType))
        {
            var normalizedVehicleType =
                vehicleType.Trim();

            query = query.Where(x =>
                x.VehicleType == normalizedVehicleType);
        }

        return await query
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.ManufacturerName)
            .ToListAsync(cancellationToken);
    }
}