using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/used-car-dataset/market-projection")]
public class UsedCarMarketProjectionController
    : ControllerBase
{
    private readonly IUsedCarMarketProjectionService _service;

    public UsedCarMarketProjectionController(
        IUsedCarMarketProjectionService service)
    {
        _service = service;
    }

    [HttpPost("{sourceId:int}")]
    public async Task<ActionResult<
        UsedCarMarketProjectionResultDto>> Project(
        int sourceId,
        [FromQuery] int batchSize = 500,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.ProjectAsync(
            sourceId,
            batchSize,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{sourceId:int}/status")]
    public async Task<ActionResult<
        UsedCarMarketProjectionResultDto>> GetStatus(
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.GetStatusAsync(
            sourceId,
            cancellationToken);

        return Ok(result);
    }
}