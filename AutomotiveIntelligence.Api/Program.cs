using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Application.Services;
using AutomotiveIntelligence.Application.Validators;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Infrastructure.Services;
using AutomotiveIntelligence.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AutomotiveIntelligenceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IMarketPriceRepository, MarketPriceRepository>();
builder.Services.AddScoped<IVehicleLookupService, VehicleLookupService>();
builder.Services.AddScoped<IValuationService, RuleBasedValuationService>();
builder.Services.AddScoped<IVehicleConditionRepository,VehicleConditionRepository>();
builder.Services.AddScoped<IValuationRepository,ValuationRepository>();
builder.Services.AddScoped<IValuationRequestValidator,ValuationRequestValidator>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<IMarketIntelligenceRepository,MarketIntelligenceRepository>();
builder.Services.AddScoped<IMarketDataImportService,MarketDataImportService>();
builder.Services.AddScoped<IMarketIntelligenceService,MarketIntelligenceService>();
builder.Services.AddScoped<IDataSourceRepository,DataSourceRepository>();
builder.Services.AddScoped<IDataSourceService,DataSourceService>();
builder.Services.AddScoped<IDataImportRepository,DataImportRepository>();
builder.Services.AddScoped<IPamaProductionRepository,PamaProductionRepository>();
builder.Services.AddScoped<IDataImportService,DataImportService>();
builder.Services.AddScoped<IDataQualityRepository, DataQualityRepository>();
builder.Services.AddScoped<IDataQualityService, DataQualityService>();
builder.Services.AddScoped<IDataQualityBySourceRepository,DataQualityBySourceRepository>();
builder.Services.AddScoped<DataQualityBySourceService>();
builder.Services.AddScoped<IExternalDataSourceConnectorResolver,ExternalDataSourceConnectorResolver>();
builder.Services.AddScoped<IExternalDataFetchService,ExternalDataFetchService>();
builder.Services.AddScoped<IExternalDataSourceConnector,PamaDataSourceConnector>();
builder.Services.AddScoped<IExternalDataSourceConnector,OicaDataSourceConnector>();
builder.Services.AddScoped<IPamaProductionImportService,PamaProductionImportService>();
builder.Services.AddScoped<PamaProductionService>();
builder.Services.AddScoped<IPamaIntelligenceRepository,PamaIntelligenceRepository>();
builder.Services.AddScoped<PamaIntelligenceService>();
builder.Services.AddScoped<IOicaProductionImportService,OicaProductionImportService>();
builder.Services.AddScoped<IOicaProductionRepository,OicaProductionRepository>();
builder.Services.AddScoped<OicaIntelligenceService>();
builder.Services.AddScoped<IDataSetProfiler, DataSetProfiler>();
builder.Services.AddScoped<IUsedCarDatasetImportService,UsedCarDatasetImportService>();
builder.Services.AddScoped<IUsedCarDatasetQualityService,UsedCarDatasetQualityService>();
builder.Services.AddScoped<IUsedCarDatasetNormalizationService,UsedCarDatasetNormalizationService>();
builder.Services.AddScoped<IUsedCarMarketProjectionService,UsedCarMarketProjectionService>();
builder.Services.AddScoped<IUsedCarDatasetVehicleMappingService,UsedCarDatasetVehicleMappingService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
