using AutomotiveIntelligence.Application.DTOs.UsedCarDataset;
using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/used-car-datasets")]
public class UsedCarDatasetController : ControllerBase
{
    private readonly IUsedCarDatasetImportService _importService;

    private readonly IUsedCarDatasetQualityService _qualityService;

    public UsedCarDatasetController(
        IUsedCarDatasetImportService importService,
        IUsedCarDatasetQualityService qualityService)
    {
        _importService = importService;
        _qualityService = qualityService;
    }

    [HttpPost("import")]
    [RequestSizeLimit(100_000_000)]
    public async Task<
        ActionResult<UsedCarDatasetImportResultDto>> Import(
        IFormFile file,
        [FromQuery] int sourceId,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(
                "A CSV file is required.");
        }

        if (sourceId <= 0)
        {
            return BadRequest(
                "A valid sourceId is required.");
        }

        await using var stream =
            file.OpenReadStream();

        var result =
            await _importService.ImportAsync(
                stream,
                file.FileName,
                sourceId,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("quality/{sourceId:int}")]
    public async Task<
        ActionResult<UsedCarDatasetQualityDto>> GetQuality(
        int sourceId,
        CancellationToken cancellationToken)
    {
        if (sourceId <= 0)
        {
            return BadRequest(
                "A valid sourceId is required.");
        }

        var result =
            await _qualityService.GetQualityAsync(
                sourceId,
                cancellationToken);

        if (result.TotalRecords == 0)
        {
            return NotFound(
                $"No used-car dataset records were found for source {sourceId}.");
        }

        return Ok(result);
    }
}