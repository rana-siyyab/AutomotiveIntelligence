using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/market-intelligence")]
public class MarketIntelligenceController
    : ControllerBase
{
    private readonly IMarketIntelligenceService
        _marketIntelligenceService;

    public MarketIntelligenceController(
        IMarketIntelligenceService marketIntelligenceService)
    {
        _marketIntelligenceService =
            marketIntelligenceService;
    }


    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        var result =
            await _marketIntelligenceService
                .GetOverviewAsync();

        return Ok(result);
    }


    [HttpGet("by-make")]
    public async Task<IActionResult> GetByMake()
    {
        var result =
            await _marketIntelligenceService
                .GetByMakeAsync();

        return Ok(result);
    }


    [HttpGet("by-model")]
    public async Task<IActionResult> GetByModel()
    {
        var result =
            await _marketIntelligenceService
                .GetByModelAsync();

        return Ok(result);
    }


    [HttpGet("by-city")]
    public async Task<IActionResult> GetByCity()
    {
        var result =
            await _marketIntelligenceService
                .GetByCityAsync();

        return Ok(result);
    }


    [HttpGet("by-year")]
    public async Task<IActionResult> GetByYear()
    {
        var result =
            await _marketIntelligenceService
                .GetByYearAsync();

        return Ok(result);
    }


    [HttpGet("price-trends")]
    public async Task<IActionResult> GetPriceTrends(
        [FromQuery] int days = 30)
    {
        days = Math.Clamp(days, 1, 365);

        var result =
            await _marketIntelligenceService
                .GetPriceTrendsAsync(days);

        return Ok(result);
    }
}