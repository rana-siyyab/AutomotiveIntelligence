using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class OicaIntelligenceService
{
    private readonly IOicaProductionRepository _repository;

    public OicaIntelligenceService(
        IOicaProductionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<OicaYearlySummaryDto>>
        GetYearlySummaryAsync(
            int? fromYear = null,
            int? toYear = null,
            string? countryName = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            fromYear,
            toYear,
            countryName,
            vehicleType,
            cancellationToken);

        return records
            .GroupBy(x => x.Year)
            .Select(group => new OicaYearlySummaryDto
            {
                Year = group.Key,
                ProductionUnits = group.Sum(x =>
                    x.ProductionUnits)
            })
            .OrderBy(x => x.Year)
            .ToList();
    }

    public async Task<IReadOnlyList<OicaCountrySummaryDto>>
        GetCountrySummaryAsync(
            int? fromYear = null,
            int? toYear = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            fromYear,
            toYear,
            null,
            vehicleType,
            cancellationToken);

        var totalProduction = records.Sum(x =>
            x.ProductionUnits);

        return records
            .GroupBy(x => x.CountryName)
            .Select(group => new OicaCountrySummaryDto
            {
                CountryName = group.Key,
                ProductionUnits = group.Sum(x =>
                    x.ProductionUnits),
                ProductionSharePercentage =
                    totalProduction == 0
                        ? 0
                        : Math.Round(
                            group.Sum(x => x.ProductionUnits)
                            * 100m
                            / totalProduction,
                            2)
            })
            .OrderByDescending(x => x.ProductionUnits)
            .ToList();
    }

    public async Task<IReadOnlyList<OicaVehicleTypeSummaryDto>>
        GetVehicleTypeSummaryAsync(
            int? fromYear = null,
            int? toYear = null,
            string? countryName = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            fromYear,
            toYear,
            countryName,
            null,
            cancellationToken);

        var totalProduction = records.Sum(x =>
            x.ProductionUnits);

        return records
            .GroupBy(x => x.VehicleType)
            .Select(group => new OicaVehicleTypeSummaryDto
            {
                VehicleType = group.Key,
                ProductionUnits = group.Sum(x =>
                    x.ProductionUnits),
                ProductionSharePercentage =
                    totalProduction == 0
                        ? 0
                        : Math.Round(
                            group.Sum(x => x.ProductionUnits)
                            * 100m
                            / totalProduction,
                            2)
            })
            .OrderByDescending(x => x.ProductionUnits)
            .ToList();
    }

    public async Task<IReadOnlyList<OicaYearlyTrendDto>>
        GetYearlyTrendAsync(
            int? fromYear = null,
            int? toYear = null,
            string? countryName = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            fromYear,
            toYear,
            countryName,
            vehicleType,
            cancellationToken);

        return records
            .GroupBy(x => x.Year)
            .Select(group => new OicaYearlyTrendDto
            {
                Year = group.Key,
                ProductionUnits = group.Sum(x =>
                    x.ProductionUnits)
            })
            .OrderBy(x => x.Year)
            .ToList();
    }

    public async Task<IReadOnlyList<OicaProductionDto>>
        GetProductionAsync(
            int? fromYear = null,
            int? toYear = null,
            string? countryName = null,
            string? vehicleType = null,
            CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            fromYear,
            toYear,
            countryName,
            vehicleType,
            cancellationToken);

        return records
            .Select(x => new OicaProductionDto
            {
                OicaProductionId =
                    x.OicaProductionId,
                SourceId = x.SourceId,
                ImportId = x.ImportId,
                CountryId = x.CountryId,
                CountryName = x.CountryName,
                VehicleType = x.VehicleType,
                Year = x.Year,
                ProductionUnits =
                    x.ProductionUnits,
                ObservationDate =
                    x.ObservationDate
            })
            .ToList();
    }
}