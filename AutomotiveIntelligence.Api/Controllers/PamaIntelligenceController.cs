using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/pama-intelligence")]
public class PamaIntelligenceController : ControllerBase
{
    private readonly PamaIntelligenceService _service;

    public PamaIntelligenceController(
        PamaIntelligenceService service)
    {
        _service = service;
    }

    [HttpGet("yearly-summary")]
    public async Task<IActionResult> GetYearlySummary(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] int? makeId = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetYearlySummaryAsync(
                fromYear,
                toYear,
                makeId,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("manufacturers")]
    public async Task<IActionResult> GetManufacturers(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] int? makeId = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetManufacturerSummaryAsync(
                fromYear,
                toYear,
                makeId,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }

    [HttpGet("monthly-trend")]
    public async Task<IActionResult> GetMonthlyTrend(
        [FromQuery] int? fromYear = null,
        [FromQuery] int? toYear = null,
        [FromQuery] int? makeId = null,
        [FromQuery] string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _service.GetMonthlyTrendAsync(
                fromYear,
                toYear,
                makeId,
                vehicleType,
                cancellationToken);

        return Ok(result);
    }
}