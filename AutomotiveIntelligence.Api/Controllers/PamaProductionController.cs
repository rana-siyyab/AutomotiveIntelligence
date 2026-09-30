using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/pama-production")]
public class PamaProductionController : ControllerBase
{
    private readonly IPamaProductionImportService _importService;
    private readonly IExternalDataFetchService _fetchService;
    private readonly PamaProductionService _service;

    public PamaProductionController(
        IPamaProductionImportService importService,
        IExternalDataFetchService fetchService,
        PamaProductionService service)
    {
        _importService = importService;
        _fetchService = fetchService;
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int? year = null,
        [FromQuery] int? makeId = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.GetAsync(
            year,
            makeId,
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

        await using var stream = file.OpenReadStream();

        var result = await _importService.ImportAsync(
            stream,
            file.FileName,
            sourceId,
            cancellationToken);

        if (!result.Success && result.ImportId == 0)
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

        var fetchResult = await _fetchService.FetchAsync(
            sourceId,
            cancellationToken);

        if (!fetchResult.Success ||
            fetchResult.Data == null)
        {
            return BadRequest(fetchResult);
        }

        await using var dataStream = fetchResult.Data;

        var importResult =
            await _importService.ImportAsync(
                dataStream,
                fetchResult.FileName ?? "pama-data.csv",
                sourceId,
                cancellationToken);

        return Ok(importResult);
    }
}