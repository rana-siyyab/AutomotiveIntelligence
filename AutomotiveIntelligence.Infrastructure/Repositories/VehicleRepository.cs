using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public VehicleRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Make>> GetActiveMakesAsync()
    {
        return await _context.Makes
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Model>> GetActiveModelsByMakeAsync(
        int makeId)
    {
        return await _context.Models
            .AsNoTracking()
            .Where(x => x.MakeId == makeId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Variant>> GetActiveVariantsByModelAsync(
        int modelId)
    {
        return await _context.Variants
            .AsNoTracking()
            .Where(x => x.ModelId == modelId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Country>> GetActiveCountriesAsync()
    {
        return await _context.Countries
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Province>> GetActiveProvincesByCountryAsync(
        int countryId)
    {
        return await _context.Provinces
            .AsNoTracking()
            .Where(x => x.CountryId == countryId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<City>> GetActiveCitiesByProvinceAsync(
        int provinceId)
    {
        return await _context.Cities
            .AsNoTracking()
            .Where(x => x.ProvinceId == provinceId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<VehicleCondition>> GetActiveVehicleConditionsAsync()
    {
        return await _context.VehicleConditions
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderByDescending(x => x.Score)
            .ToListAsync();
    }
}