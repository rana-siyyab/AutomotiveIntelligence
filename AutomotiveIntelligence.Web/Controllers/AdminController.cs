using AutomotiveIntelligence.Web.Models;
using AutomotiveIntelligence.Web.Models.Admin;
using AutomotiveIntelligence.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Web.Controllers;

public class AdminController : Controller
{
    private readonly IAutomotiveApiClient _apiClient;

    public AdminController(
        IAutomotiveApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public async Task<IActionResult> DataImport()
    {
        var dataSources =
            await _apiClient.GetDataSourcesAsync();

        var model = new MarketDataImportViewModel
        {
            DataSources = dataSources,

            SourceId = dataSources
                .FirstOrDefault()?
                .SourceId ?? 0
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DataImport(
        MarketDataImportViewModel model)
    {
        var dataSources =
            await _apiClient.GetDataSourcesAsync();

        model.DataSources = dataSources;

        if (model.SourceId <= 0)
        {
            ModelState.AddModelError(
                nameof(model.SourceId),
                "Please select a data source.");
        }

        if (model.File == null ||
            model.File.Length == 0)
        {
            ModelState.AddModelError(
                nameof(model.File),
                "Please select a CSV file.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!model.File!.FileName.EndsWith(
                ".csv",
                StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError(
                nameof(model.File),
                "Only CSV files are supported.");

            return View(model);
        }

        try
        {
            model.Result =
                await _apiClient.ImportMarketDataAsync(
                    model.SourceId,
                    model.File);

            model.File = null;

            return View(model);
        }
        catch (HttpRequestException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The data import service could not be reached.");

            return View(model);
        }
        catch (Exception)
        {
            ModelState.AddModelError(
                string.Empty,
                "An unexpected error occurred while importing the file.");

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ImportHistory(
    int pageNumber = 1,
    int pageSize = 20)
    {
        pageNumber = Math.Max(1, pageNumber);

        pageSize = Math.Clamp(
            pageSize,
            1,
            100);

        var imports =
            await _apiClient.GetDataImportsAsync(
                pageNumber,
                pageSize);

        var model = new DataImportHistoryViewModel
        {
            Imports = imports,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return View(model);
    }
    [HttpGet]
    public async Task<IActionResult> DataQuality()
    {
        var dataQuality = await _apiClient.GetDataQualityAsync();

        var sourceQuality =
            await _apiClient.GetDataQualityBySourceAsync();

        ViewBag.SourceQuality = sourceQuality;

        return View(dataQuality);
    }

    [HttpGet]
    public async Task<IActionResult> DataSources()
    {
        var sources = await _apiClient.GetDataSourcesAsync();

        var model = new DataSourcesViewModel
        {
            Sources = sources
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DataSources(
    DataSourcesViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Sources =
                await _apiClient.GetDataSourcesAsync();

            return View(model);
        }

        try
        {
            await _apiClient.CreateDataSourceAsync(model.Create);

            return RedirectToAction(nameof(DataSources));
        }
        catch (HttpRequestException)
        {
            ModelState.AddModelError(
                string.Empty,
                "The data source service could not be reached.");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);
        }

        model.Sources =
            await _apiClient.GetDataSourcesAsync();

        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var marketOverviewTask =
            _apiClient.GetMarketOverviewAsync();

        var dataQualityTask =
            _apiClient.GetDataQualityAsync();

        var priceTrendsTask =
            _apiClient.GetPriceTrendsAsync(30);

        var popularModelsTask =
            _apiClient.GetPopularModelsAsync(10);

        var pamaYearlySummaryTask =
            _apiClient.GetPamaYearlySummaryAsync();

        var oicaYearlySummaryTask =
            _apiClient.GetOicaYearlySummaryAsync();

        await Task.WhenAll(
            marketOverviewTask,
            dataQualityTask,
            priceTrendsTask,
            popularModelsTask,
            pamaYearlySummaryTask,
            oicaYearlySummaryTask);

        var model = new AdminDashboardViewModel
        {
            MarketOverview =
                await marketOverviewTask,

            DataQuality =
                await dataQualityTask,

            PriceTrends =
                await priceTrendsTask,

            PopularModels =
                await popularModelsTask,

            PamaYearlySummary =
                await pamaYearlySummaryTask,

            OicaYearlySummary =
                await oicaYearlySummaryTask
        };

        return View(model);
    }
}