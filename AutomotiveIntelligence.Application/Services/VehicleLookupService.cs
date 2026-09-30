using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class VehicleLookupService : IVehicleLookupService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleLookupService(
        IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<IReadOnlyList<MakeDto>> GetActiveMakesAsync()
    {
        var makes = await _vehicleRepository.GetActiveMakesAsync();

        return makes
            .Select(x => new MakeDto
            {
                MakeId = x.MakeId,
                Name = x.Name
            })
            .ToList();
    }

    public async Task<IReadOnlyList<ModelDto>> GetActiveModelsByMakeAsync(
        int makeId)
    {
        var models = await _vehicleRepository
            .GetActiveModelsByMakeAsync(makeId);

        return models
            .Select(x => new ModelDto
            {
                ModelId = x.ModelId,
                MakeId = x.MakeId,
                Name = x.Name,
                BodyType = x.BodyType
            })
            .ToList();
    }

    public async Task<IReadOnlyList<VariantDto>> GetActiveVariantsByModelAsync(
        int modelId)
    {
        var variants = await _vehicleRepository
            .GetActiveVariantsByModelAsync(modelId);

        return variants
            .Select(x => new VariantDto
            {
                VariantId = x.VariantId,
                ModelId = x.ModelId,
                Name = x.Name,
                EngineCapacity = x.EngineCapacity,
                Transmission = x.Transmission,
                FuelType = x.FuelType,
                DriveType = x.DriveType,
                SeatingCapacity = x.SeatingCapacity
            })
            .ToList();
    }

    public async Task<IReadOnlyList<CountryDto>> GetActiveCountriesAsync()
    {
        var countries =
            await _vehicleRepository.GetActiveCountriesAsync();

        return countries
            .Select(x => new CountryDto
            {
                CountryId = x.CountryId,
                Name = x.Name,
                Code = x.Code
            })
            .ToList();
    }

    public async Task<IReadOnlyList<ProvinceDto>> GetActiveProvincesByCountryAsync(
        int countryId)
    {
        var provinces =
            await _vehicleRepository
                .GetActiveProvincesByCountryAsync(countryId);

        return provinces
            .Select(x => new ProvinceDto
            {
                ProvinceId = x.ProvinceId,
                CountryId = x.CountryId,
                Name = x.Name
            })
            .ToList();
    }

    public async Task<IReadOnlyList<CityDto>> GetActiveCitiesByProvinceAsync(
        int provinceId)
    {
        var cities =
            await _vehicleRepository
                .GetActiveCitiesByProvinceAsync(provinceId);

        return cities
            .Select(x => new CityDto
            {
                CityId = x.CityId,
                ProvinceId = x.ProvinceId,
                Name = x.Name
            })
            .ToList();
    }

    public async Task<IReadOnlyList<VehicleConditionDto>> GetActiveVehicleConditionsAsync()
    {
        var conditions =
            await _vehicleRepository
                .GetActiveVehicleConditionsAsync();

        return conditions
            .Select(x => new VehicleConditionDto
            {
                ConditionId = x.ConditionId,
                Name = x.Name,
                Score = x.Score,
                Description = x.Description
            })
            .ToList();
    }
}