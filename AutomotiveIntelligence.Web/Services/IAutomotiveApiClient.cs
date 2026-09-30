using AutomotiveIntelligence.Web.Models;
using AutomotiveIntelligence.Web.Models.Admin;
using AutomotiveIntelligence.Web.Models.Dashboard;
using AutomotiveIntelligence.Web.Models.MarketIntelligence;
using Microsoft.AspNetCore.Http;

namespace AutomotiveIntelligence.Web.Services;

public interface IAutomotiveApiClient
{
    Task<IReadOnlyList<RecentValuationViewModel>>
        GetRecentValuationsAsync(
            int pageNumber = 1,
            int pageSize = 10);

    Task<ValuationResultViewModel?>
        CalculateValuationAsync(
            ValuationRequestViewModel request);

    Task<IReadOnlyList<MakeViewModel>>
        GetMakesAsync();

    Task<IReadOnlyList<ModelViewModel>>
        GetModelsAsync(int makeId);

    Task<IReadOnlyList<VariantViewModel>>
        GetVariantsAsync(int modelId);

    Task<IReadOnlyList<CountryViewModel>>
        GetCountriesAsync();

    Task<IReadOnlyList<ProvinceViewModel>>
        GetProvincesAsync(int countryId);

    Task<IReadOnlyList<CityViewModel>>
        GetCitiesAsync(int provinceId);

    Task<IReadOnlyList<VehicleConditionViewModel>>
        GetConditionsAsync();


    // Dashboard

    Task<MarketOverviewViewModel>
        GetMarketOverviewAsync();

    Task<IReadOnlyList<PriceTrendViewModel>>
        GetPriceTrendsAsync(int days = 30);

    Task<IReadOnlyList<PopularModelViewModel>>
        GetPopularModelsAsync(int count = 10);


    // Market Intelligence

    Task<MarketIntelligenceOverviewViewModel>
        GetMarketIntelligenceOverviewAsync();

    Task<IReadOnlyList<MarketPriceByMakeViewModel>>
        GetMarketPricesByMakeAsync();

    Task<IReadOnlyList<MarketPriceByModelViewModel>>
        GetMarketPricesByModelAsync();

    Task<IReadOnlyList<MarketPriceByCityViewModel>>
        GetMarketPricesByCityAsync();

    Task<IReadOnlyList<MarketPriceByYearViewModel>>
        GetMarketPricesByYearAsync();

    Task<IReadOnlyList<MarketPriceTrendViewModel>>
        GetMarketIntelligencePriceTrendsAsync(
            int days = 30);

    //  Admin
    Task<MarketDataImportResultViewModel> ImportMarketDataAsync(
    int sourceId,
    IFormFile file);
    Task<IReadOnlyList<DataSourceViewModel>>
    GetDataSourcesAsync();
    Task<IReadOnlyList<DataImportViewModel>>
    GetDataImportsAsync(
        int pageNumber = 1,
        int pageSize = 20);
    Task<DataQualityViewModel> GetDataQualityAsync();
    Task<IReadOnlyList<DataQualityBySourceViewModel>>
    GetDataQualityBySourceAsync();
    Task<IReadOnlyList<PamaYearlySummaryViewModel>>
    GetPamaYearlySummaryAsync();

    Task<IReadOnlyList<OicaYearlySummaryViewModel>>
        GetOicaYearlySummaryAsync();

    // Data Source 
    Task<DataSourceViewModel> CreateDataSourceAsync(
    CreateDataSourceViewModel model);
}