using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IVehicleLookupService
{
    Task<IReadOnlyList<MakeDto>> GetActiveMakesAsync();

    Task<IReadOnlyList<ModelDto>> GetActiveModelsByMakeAsync(
        int makeId);

    Task<IReadOnlyList<VariantDto>> GetActiveVariantsByModelAsync(
        int modelId);

    Task<IReadOnlyList<CountryDto>> GetActiveCountriesAsync();

    Task<IReadOnlyList<ProvinceDto>> GetActiveProvincesByCountryAsync(
        int countryId);

    Task<IReadOnlyList<CityDto>> GetActiveCitiesByProvinceAsync(
        int provinceId);

    Task<IReadOnlyList<VehicleConditionDto>> GetActiveVehicleConditionsAsync();
}