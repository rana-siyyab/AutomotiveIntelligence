using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class PamaIntelligenceRepository
    : IPamaIntelligenceRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public PamaIntelligenceRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PamaProduction>> GetAllAsync(
        int? fromYear = null,
        int? toYear = null,
        int? makeId = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.PamaProductions
            .AsNoTracking()
            .AsQueryable();

        if (fromYear.HasValue)
        {
            query = query.Where(
                x => x.Year >= fromYear.Value);
        }

        if (toYear.HasValue)
        {
            query = query.Where(
                x => x.Year <= toYear.Value);
        }

        if (makeId.HasValue)
        {
            query = query.Where(
                x => x.MakeId == makeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(vehicleType))
        {
            var normalizedVehicleType =
                vehicleType.Trim();

            query = query.Where(
                x => x.VehicleType == normalizedVehicleType);
        }

        return await query
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ThenBy(x => x.ManufacturerName)
            .ToListAsync(cancellationToken);
    }
}