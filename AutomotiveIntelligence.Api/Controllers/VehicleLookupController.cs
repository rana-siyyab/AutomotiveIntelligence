using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/vehicle-lookup")]
public class VehicleLookupController : ControllerBase
{
    private readonly IVehicleLookupService _vehicleLookupService;

    public VehicleLookupController(
        IVehicleLookupService vehicleLookupService)
    {
        _vehicleLookupService = vehicleLookupService;
    }

    [HttpGet("makes")]
    public async Task<IActionResult> GetMakesAsync()
    {
        var makes = await _vehicleLookupService.GetActiveMakesAsync();

        return Ok(makes);
    }

    [HttpGet("models/{makeId:int}")]
    public async Task<IActionResult> GetModelsAsync(int makeId)
    {
        var models =
            await _vehicleLookupService.GetActiveModelsByMakeAsync(makeId);

        return Ok(models);
    }

    [HttpGet("variants/{modelId:int}")]
    public async Task<IActionResult> GetVariantsAsync(int modelId)
    {
        var variants =
            await _vehicleLookupService.GetActiveVariantsByModelAsync(modelId);

        return Ok(variants);
    }
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountriesAsync()
    {
        var countries =
            await _vehicleLookupService.GetActiveCountriesAsync();

        return Ok(countries);
    }

    [HttpGet("provinces/{countryId:int}")]
    public async Task<IActionResult> GetProvincesAsync(int countryId)
    {
        var provinces =
            await _vehicleLookupService
                .GetActiveProvincesByCountryAsync(countryId);

        return Ok(provinces);
    }

    [HttpGet("cities/{provinceId:int}")]
    public async Task<IActionResult> GetCitiesAsync(int provinceId)
    {
        var cities =
            await _vehicleLookupService
                .GetActiveCitiesByProvinceAsync(provinceId);

        return Ok(cities);
    }

    [HttpGet("conditions")]
    public async Task<IActionResult> GetConditionsAsync()
    {
        var conditions =
            await _vehicleLookupService
                .GetActiveVehicleConditionsAsync();

        return Ok(conditions);
    }
}