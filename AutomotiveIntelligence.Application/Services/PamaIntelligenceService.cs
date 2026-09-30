using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class PamaIntelligenceService
{
    private readonly IPamaIntelligenceRepository _repository;

    public PamaIntelligenceService(
        IPamaIntelligenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PamaYearlySummaryDto>>
        GetYearlySummaryAsync(
            int? fromYear = null,
            int? toYear = null,
            int? makeId = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAllAsync(
            fromYear,
            toYear,
            makeId,
            vehicleType,
            cancellationToken);

        return records
            .GroupBy(x => x.Year)
            .OrderBy(x => x.Key)
            .Select(group =>
            {
                var production = group.Sum(
                    x => x.ProductionUnits);

                var sales = group.Sum(
                    x => x.SalesUnits);

                return new PamaYearlySummaryDto
                {
                    Year = group.Key,
                    ProductionUnits = production,
                    SalesUnits = sales,
                    ProductionVsSalesRatio =
                        sales == 0
                            ? 0
                            : Math.Round(
                                (decimal)production / sales,
                                2)
                };
            })
            .ToList();
    }

    public async Task<IReadOnlyList<PamaManufacturerSummaryDto>>
        GetManufacturerSummaryAsync(
            int? fromYear = null,
            int? toYear = null,
            int? makeId = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAllAsync(
            fromYear,
            toYear,
            makeId,
            vehicleType,
            cancellationToken);

        var totalProduction = records.Sum(
            x => x.ProductionUnits);

        var totalSales = records.Sum(
            x => x.SalesUnits);

        return records
            .GroupBy(x => new
            {
                x.MakeId,
                x.ManufacturerName
            })
            .Select(group =>
            {
                var production = group.Sum(
                    x => x.ProductionUnits);

                var sales = group.Sum(
                    x => x.SalesUnits);

                return new PamaManufacturerSummaryDto
                {
                    MakeId = group.Key.MakeId,
                    ManufacturerName =
                        group.Key.ManufacturerName,

                    ProductionUnits = production,
                    SalesUnits = sales,

                    ProductionSharePercentage =
                        totalProduction == 0
                            ? 0
                            : Math.Round(
                                (decimal)production /
                                totalProduction * 100,
                                2),

                    SalesSharePercentage =
                        totalSales == 0
                            ? 0
                            : Math.Round(
                                (decimal)sales /
                                totalSales * 100,
                                2)
                };
            })
            .OrderByDescending(
                x => x.ProductionUnits)
            .ToList();
    }

    public async Task<IReadOnlyList<PamaMonthlyTrendDto>>
        GetMonthlyTrendAsync(
            int? fromYear = null,
            int? toYear = null,
            int? makeId = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAllAsync(
            fromYear,
            toYear,
            makeId,
            vehicleType,
            cancellationToken);

        return records
            .GroupBy(x => new
            {
                x.Year,
                x.Month
            })
            .OrderBy(group => group.Key.Year)
            .ThenBy(group => group.Key.Month)
            .Select(group => new PamaMonthlyTrendDto
            {
                Year = group.Key.Year,
                Month = group.Key.Month,
                MonthName = new DateTime(
                    group.Key.Year,
                    group.Key.Month,
                    1)
                    .ToString("MMM"),

                ProductionUnits = group.Sum(
                    x => x.ProductionUnits),

                SalesUnits = group.Sum(
                    x => x.SalesUnits)
            })
            .ToList();
    }
}