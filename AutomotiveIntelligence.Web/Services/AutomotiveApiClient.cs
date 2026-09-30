using AutomotiveIntelligence.Web.Models;
using AutomotiveIntelligence.Web.Models.Admin;
using AutomotiveIntelligence.Web.Models.Dashboard;
using AutomotiveIntelligence.Web.Models.MarketIntelligence;

namespace AutomotiveIntelligence.Web.Services;

public class AutomotiveApiClient : IAutomotiveApiClient
{
    private readonly HttpClient _httpClient;

    public AutomotiveApiClient(
        IHttpClientFactory httpClientFactory)
    {
        _httpClient =
            httpClientFactory.CreateClient(
                "AutomotiveIntelligenceApi");
    }

    public async Task<IReadOnlyList<RecentValuationViewModel>>
        GetRecentValuationsAsync(
            int pageNumber = 1,
            int pageSize = 10)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/valuation/recent?pageNumber={pageNumber}&pageSize={pageSize}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<RecentValuationViewModel>>() ?? [];
    }

    public async Task<ValuationResultViewModel?>
        CalculateValuationAsync(
            ValuationRequestViewModel request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/v1/valuation/calculate",
            request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            ValuationResultViewModel>();
    }

    public async Task<IReadOnlyList<MakeViewModel>>
        GetMakesAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/v1/vehicle-lookup/makes");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<MakeViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<ModelViewModel>>
        GetModelsAsync(int makeId)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/vehicle-lookup/models/{makeId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<ModelViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<VariantViewModel>>
        GetVariantsAsync(int modelId)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/vehicle-lookup/variants/{modelId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<VariantViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<CountryViewModel>>
        GetCountriesAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/v1/vehicle-lookup/countries");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<CountryViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<ProvinceViewModel>>
        GetProvincesAsync(int countryId)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/vehicle-lookup/provinces/{countryId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<ProvinceViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<CityViewModel>>
        GetCitiesAsync(int provinceId)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/vehicle-lookup/cities/{provinceId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<CityViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<VehicleConditionViewModel>>
        GetConditionsAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/v1/vehicle-lookup/conditions");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<VehicleConditionViewModel>>() ?? [];
    }

    public async Task<MarketOverviewViewModel>
        GetMarketOverviewAsync()
    {
        var response = await _httpClient.GetAsync(
            "api/v1/dashboard/market-overview");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            MarketOverviewViewModel>()
            ?? new MarketOverviewViewModel();
    }

    public async Task<IReadOnlyList<PriceTrendViewModel>>
        GetPriceTrendsAsync(int days = 30)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/dashboard/price-trends?days={days}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<PriceTrendViewModel>>() ?? [];
    }

    public async Task<IReadOnlyList<PopularModelViewModel>>
        GetPopularModelsAsync(int count = 10)
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/dashboard/popular-models?count={count}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<
            List<PopularModelViewModel>>() ?? [];
    }

    public async Task<MarketIntelligenceOverviewViewModel>
    GetMarketIntelligenceOverviewAsync()
    {
        var response =
            await _httpClient.GetAsync(
                "api/v1/market-intelligence/overview");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<MarketIntelligenceOverviewViewModel>()
            ?? new MarketIntelligenceOverviewViewModel();
    }


    public async Task<IReadOnlyList<MarketPriceByMakeViewModel>>
        GetMarketPricesByMakeAsync()
    {
        var response =
            await _httpClient.GetAsync(
                "api/v1/market-intelligence/by-make");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<
                List<MarketPriceByMakeViewModel>>()
            ?? [];
    }


    public async Task<IReadOnlyList<MarketPriceByModelViewModel>>
        GetMarketPricesByModelAsync()
    {
        var response =
            await _httpClient.GetAsync(
                "api/v1/market-intelligence/by-model");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<
                List<MarketPriceByModelViewModel>>()
            ?? [];
    }


    public async Task<IReadOnlyList<MarketPriceByCityViewModel>>
        GetMarketPricesByCityAsync()
    {
        var response =
            await _httpClient.GetAsync(
                "api/v1/market-intelligence/by-city");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<
                List<MarketPriceByCityViewModel>>()
            ?? [];
    }


    public async Task<IReadOnlyList<MarketPriceByYearViewModel>>
        GetMarketPricesByYearAsync()
    {
        var response =
            await _httpClient.GetAsync(
                "api/v1/market-intelligence/by-year");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<
                List<MarketPriceByYearViewModel>>()
            ?? [];
    }


    public async Task<IReadOnlyList<MarketPriceTrendViewModel>>
        GetMarketIntelligencePriceTrendsAsync(
            int days = 30)
    {
        days = Math.Clamp(days, 1, 365);

        var response =
            await _httpClient.GetAsync(
                $"api/v1/market-intelligence/price-trends?days={days}");

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<
                List<MarketPriceTrendViewModel>>()
            ?? [];
    }

    public async Task<MarketDataImportResultViewModel>
    ImportMarketDataAsync(
        int sourceId,
        IFormFile file)
    {
        using var content = new MultipartFormDataContent();

        content.Add(
            new StringContent(sourceId.ToString()),
            "sourceId");

        await using var stream = file.OpenReadStream();

        var fileContent = new StreamContent(stream);

        fileContent.Headers.ContentType =
            new System.Net.Http.Headers.MediaTypeHeaderValue(
                "text/csv");

        content.Add(
            fileContent,
            "file",
            file.FileName);

        var response = await _httpClient.PostAsync(
            "api/v1/market-data/import",
            content);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<MarketDataImportResultViewModel>();

        return result
            ?? throw new InvalidOperationException(
                "The market data import API returned an empty response.");
    }
    public async Task<IReadOnlyList<DataSourceViewModel>>
    GetDataSourcesAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<DataSourceViewModel>>(
                    "api/v1/data-sources");

        return result ?? [];
    }

    public async Task<IReadOnlyList<DataImportViewModel>>
    GetDataImportsAsync(
        int pageNumber = 1,
        int pageSize = 20)
    {
        var url =
            $"api/v1/data-imports" +
            $"?pageNumber={pageNumber}" +
            $"&pageSize={pageSize}";

        var result =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<DataImportViewModel>>(url);

        return result ?? [];
    }
    public async Task<DataQualityViewModel> GetDataQualityAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<DataQualityViewModel>(
                "api/v1/data-quality/overview");

        return result
            ?? throw new InvalidOperationException(
                "The data quality API returned an empty response.");
    }
    public async Task<IReadOnlyList<DataQualityBySourceViewModel>>
    GetDataQualityBySourceAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<DataQualityBySourceViewModel>>(
                    "api/v1/data-quality/by-source");

        return result ?? [];
    }
    public async Task<DataSourceViewModel> CreateDataSourceAsync(
    CreateDataSourceViewModel model)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/v1/data-sources",
            model);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<DataSourceViewModel>();

        return result
            ?? throw new InvalidOperationException(
                "The data source API returned an empty response.");
    }

    public async Task<IReadOnlyList<PamaYearlySummaryViewModel>>
    GetPamaYearlySummaryAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<PamaYearlySummaryViewModel>>(
                    "api/v1/pama-intelligence/yearly-summary");

        return result ?? [];
    }


    public async Task<IReadOnlyList<OicaYearlySummaryViewModel>>
        GetOicaYearlySummaryAsync()
    {
        var result =
            await _httpClient.GetFromJsonAsync<
                IReadOnlyList<OicaYearlySummaryViewModel>>(
                    "api/v1/oica-production/yearly-summary");

        return result ?? [];
    }
}