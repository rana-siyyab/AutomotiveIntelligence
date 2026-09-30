using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/market-data")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataImportService
        _importService;

    public MarketDataController(
        IMarketDataImportService importService)
    {
        _importService =
            importService;
    }


    [HttpPost("import")]
    [RequestSizeLimit(10 * 1024 * 1024)]
    public async Task<IActionResult> Import(
        [FromForm] IFormFile file,
        [FromForm] int sourceId)
    {
        if (file == null ||
            file.Length == 0)
        {
            return BadRequest(
                new
                {
                    message =
                        "A CSV file is required."
                });
        }


        if (!file.FileName
            .EndsWith(
                ".csv",
                StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(
                new
                {
                    message =
                        "Only CSV files are supported."
                });
        }


        if (sourceId <= 0)
        {
            return BadRequest(
                new
                {
                    message =
                        "A valid sourceId is required."
                });
        }


        await using var stream =
            file.OpenReadStream();


        var result =
            await _importService.ImportAsync(
                stream,
                file.FileName,
                sourceId);


        return Ok(result);
    }
}