using AutomotiveIntelligence.Web.Models;
using AutomotiveIntelligence.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Web.Controllers;

public class ValuationController : Controller
{
    private readonly IAutomotiveApiClient _apiClient;

    public ValuationController(IAutomotiveApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // ManufacturingYear is intentionally NOT populated.
        // Year is an optional valuation input.
        var model = new ValuationRequestViewModel();

        await PopulateLookupDataAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Models(int makeId)
    {
        var models = await _apiClient.GetModelsAsync(makeId);

        return Json(models);
    }

    [HttpGet]
    public async Task<IActionResult> Variants(int modelId)
    {
        var variants = await _apiClient.GetVariantsAsync(modelId);

        return Json(variants);
    }

    [HttpGet]
    public async Task<IActionResult> Provinces(int countryId)
    {
        var provinces = await _apiClient.GetProvincesAsync(countryId);

        return Json(provinces);
    }

    [HttpGet]
    public async Task<IActionResult> Cities(int provinceId)
    {
        var cities = await _apiClient.GetCitiesAsync(provinceId);

        return Json(cities);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Calculate(
        ValuationRequestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateLookupDataAsync();

            return View("Index", model);
        }

        try
        {
            var result =
                await _apiClient.CalculateValuationAsync(model);

            if (result == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Unable to calculate the vehicle valuation.");

                await PopulateLookupDataAsync();

                return View("Index", model);
            }

            return View("Result", result);
        }
        catch (HttpRequestException)
        {
            ModelState.AddModelError(
                string.Empty,
                "Unable to connect to the valuation service.");

            await PopulateLookupDataAsync();

            return View("Index", model);
        }
    }

    private async Task PopulateLookupDataAsync()
    {
        ViewBag.Makes =
            await _apiClient.GetMakesAsync();

        ViewBag.Countries =
            await _apiClient.GetCountriesAsync();

        ViewBag.Conditions =
            await _apiClient.GetConditionsAsync();
    }
}