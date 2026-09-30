using AutomotiveIntelligence.Application.DTOs.Dashboard;
using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;

    public DashboardController(
        DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("market-overview")]
    public async Task<ActionResult<MarketOverviewDto>>
        GetMarketOverview()
    {
        var result =
            await _dashboardService.GetMarketOverviewAsync();

        return Ok(result);
    }

    [HttpGet("price-trends")]
    public async Task<ActionResult<IReadOnlyList<PriceTrendDto>>>
        GetPriceTrends([FromQuery] int days = 30)
    {
        var result =
            await _dashboardService.GetPriceTrendsAsync(days);

        return Ok(result);
    }

    [HttpGet("popular-models")]
    public async Task<ActionResult<IReadOnlyList<PopularModelDto>>>
        GetPopularModels([FromQuery] int count = 10)
    {
        var result =
            await _dashboardService.GetPopularModelsAsync(count);

        return Ok(result);
    }
}