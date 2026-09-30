using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/used-car-dataset/vehicle-mapping")]
public class UsedCarDatasetVehicleMappingController
    : ControllerBase
{
    private readonly IUsedCarDatasetVehicleMappingService _service;

    public UsedCarDatasetVehicleMappingController(
        IUsedCarDatasetVehicleMappingService service)
    {
        _service = service;
    }

    [HttpPost("{sourceId:int}")]
    public async Task<ActionResult<
        UsedCarDatasetVehicleMappingResultDto>> Map(
        int sourceId,
        [FromQuery] int batchSize = 500,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.MapAsync(
            sourceId,
            batchSize,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{sourceId:int}/status")]
    public async Task<ActionResult<
        UsedCarDatasetVehicleMappingResultDto>> GetStatus(
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        var result = await _service.GetStatusAsync(
            sourceId,
            cancellationToken);

        return Ok(result);
    }
}