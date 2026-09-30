using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IVehicleRepository
{
    Task<IReadOnlyList<Make>> GetActiveMakesAsync();

    Task<IReadOnlyList<Model>> GetActiveModelsByMakeAsync(
        int makeId);

    Task<IReadOnlyList<Variant>> GetActiveVariantsByModelAsync(
        int modelId);

    Task<IReadOnlyList<Country>> GetActiveCountriesAsync();

    Task<IReadOnlyList<Province>> GetActiveProvincesByCountryAsync(
        int countryId);

    Task<IReadOnlyList<City>> GetActiveCitiesByProvinceAsync(
        int provinceId);

    Task<IReadOnlyList<VehicleCondition>> GetActiveVehicleConditionsAsync();
}