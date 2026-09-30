using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/oica-production")]
public class OicaProductionController : ControllerBase
{
    private readonly IOicaProductionImportService _importService;
    private readonly IExternalDataFetchService _fetchService;
    private readonly OicaIntelligenceService _service;

    public OicaProductionController(
        IOicaProductionImportService importService,
        IExternalDataFetchService fetchService,
        OicaIntelligenceService service)
    {
        _importService = importService;
        _fetchService = fetchService;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] string? countryName = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.GetProductionAsync(
            fromYear,
            toYear,
            countryName,
            vehicleType,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("import")]
    public async Task<IActionResult> Import(
        IFormFile file,
        [FromQuery] int sourceId,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new
            {
                message = "A CSV file is required."
            });
        }

        if (sourceId <= 0)
        {
            return BadRequest(new
            {
                message = "A valid sourceId is required."
            });
        }

        await using var stream =
            file.OpenReadStream();

        var result =
            await _importService.ImportAsync(
                stream,
                file.FileName,
                sourceId,
                cancellationToken);

        if (!result.Success &&
            result.ImportId == 0)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("fetch-and-import")]
    public async Task<IActionResult> FetchAndImport(
        [FromQuery] int sourceId,
        CancellationToken cancellationToken)
    {
        if (sourceId <= 0)
        {
            return BadRequest(new
            {
                message = "A valid sourceId is required."
            });
        }

        var fetchResult =
            await _fetchService.FetchAsync(
                sourceId,
                cancellationToken);

        if (!fetchResult.Success ||
            fetchResult.Data == null)
        {
            return BadRequest(fetchResult);
        }

        await using var dataStream =
            fetchResult.Data;

        var importResult =
            await _importService.ImportAsync(
                dataStream,
                fetchResult.FileName ?? "oica-data.csv",
                sourceId,
                cancellationToken);

        return Ok(importResult);
    }

    [HttpGet("yearly-summary")]
    public async Task<IActionResult> GetYearlySummary(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] string? countryName = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetYearlySummaryAsync(
                fromYear,
                toYear,
                countryName,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetCountrySummaryAsync(
                fromYear,
                toYear,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("vehicle-types")]
    public async Task<IActionResult> GetVehicleTypes(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] string? countryName = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetVehicleTypeSummaryAsync(
                fromYear,
                toYear,
                countryName,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("yearly-trend")]
    public async Task<IActionResult> GetYearlyTrend(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] string? countryName = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetYearlyTrendAsync(
                fromYear,
                toYear,
                countryName,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }
}