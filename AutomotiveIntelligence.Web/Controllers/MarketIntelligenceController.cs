using AutomotiveIntelligence.Web.Models.MarketIntelligence;
using AutomotiveIntelligence.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Web.Controllers;

public class MarketIntelligenceController : Controller
{
    private readonly IAutomotiveApiClient _apiClient;

    public MarketIntelligenceController(
        IAutomotiveApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new MarketIntelligenceViewModel
        {
            Overview =
                await _apiClient
                    .GetMarketIntelligenceOverviewAsync(),

            ByMake =
                await _apiClient
                    .GetMarketPricesByMakeAsync(),

            ByModel =
                await _apiClient
                    .GetMarketPricesByModelAsync(),

            ByCity =
                await _apiClient
                    .GetMarketPricesByCityAsync(),

            ByYear =
                await _apiClient
                    .GetMarketPricesByYearAsync(),

            PriceTrends =
                await _apiClient
                    .GetMarketIntelligencePriceTrendsAsync(30)
        };

        return View(model);
    }
}