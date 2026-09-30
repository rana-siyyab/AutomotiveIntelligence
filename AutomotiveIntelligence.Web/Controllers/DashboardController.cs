using AutomotiveIntelligence.Web.Models.Dashboard;
using AutomotiveIntelligence.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IAutomotiveApiClient _apiClient;

    public DashboardController(
        IAutomotiveApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            MarketOverview =
                await _apiClient.GetMarketOverviewAsync(),

            PriceTrends =
                await _apiClient.GetPriceTrendsAsync(30),

            PopularModels =
                await _apiClient.GetPopularModelsAsync(10),

            RecentValuations =
                await _apiClient.GetRecentValuationsAsync(1, 10)
        };

        return View(model);
    }
}