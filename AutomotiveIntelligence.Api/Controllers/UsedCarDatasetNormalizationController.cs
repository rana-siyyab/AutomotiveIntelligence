using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/used-car-dataset/normalization")]
public class UsedCarDatasetNormalizationController
    : ControllerBase
{
    private readonly
        IUsedCarDatasetNormalizationService
        _service;

    public UsedCarDatasetNormalizationController(
        IUsedCarDatasetNormalizationService service)
    {
        _service = service;
    }

    [HttpPost("{sourceId:int}")]
    public async Task<ActionResult<
        UsedCarDatasetNormalizationResultDto>>
        Normalize(
            int sourceId,
            [FromQuery] int batchSize = 1000,
            CancellationToken cancellationToken = default)
    {
        var result =
            await _service.NormalizeAsync(
                sourceId,
                batchSize,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("{sourceId:int}/status")]
    public async Task<ActionResult<
        UsedCarDatasetNormalizationResultDto>>
        GetStatus(
            int sourceId,
            CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetStatusAsync(
                sourceId,
                cancellationToken);

        return Ok(result);
    }
}