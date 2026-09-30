using AutomotiveIntelligence.Application.DTOs.UsedCarDataset;
using AutomotiveIntelligence.Application.Services;
using AutomotiveIntelligence.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class UsedCarDatasetQualityService
    : IUsedCarDatasetQualityService
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public UsedCarDatasetQualityService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<UsedCarDatasetQualityDto> GetQualityAsync(
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
    SELECT
        COUNT_BIG(*) AS TotalRecords,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithPrice,

        SUM(CASE
            WHEN AskingPrice IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutPrice,

        SUM(CASE
            WHEN ManufacturingYear IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithYear,

        SUM(CASE
            WHEN ManufacturingYear IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutYear,

        SUM(CASE
            WHEN Mileage IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithMileage,

        SUM(CASE
            WHEN Mileage IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutMileage,

        SUM(CASE
            WHEN EngineCapacity IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithEngineCapacity,

        SUM(CASE
            WHEN EngineCapacity IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutEngineCapacity,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(MakeName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithMake,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(MakeName)), '') IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutMake,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(ModelName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithModel,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(ModelName)), '') IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutModel,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(VariantName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithVariant,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(VariantName)), '') IS NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithoutVariant,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(CityName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithCity,

        SUM(CASE
            WHEN NULLIF(LTRIM(RTRIM(ProvinceName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS RecordsWithProvince,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice <= 0
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS InvalidPriceRecords,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
                 AND AskingPrice < 50000
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS SuspiciousLowPriceRecords,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 100000000
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS SuspiciousHighPriceRecords,

        SUM(CASE
            WHEN Mileage IS NOT NULL
                 AND Mileage < 0
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS InvalidMileageRecords,

        SUM(CASE
            WHEN Mileage IS NOT NULL
                 AND Mileage > 1000000
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS SuspiciousHighMileageRecords,

        SUM(CASE
            WHEN ManufacturingYear IS NOT NULL
                 AND (
                     ManufacturingYear < 1950
                     OR ManufacturingYear >
                        YEAR(GETUTCDATE()) + 1
                 )
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS InvalidYearRecords,

        SUM(CASE
            WHEN EngineCapacity IS NOT NULL
                 AND EngineCapacity <= 0
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS InvalidEngineCapacityRecords,

        SUM(CASE
            WHEN EngineCapacity IS NOT NULL
                 AND (
                     EngineCapacity < 400
                     OR EngineCapacity > 10000
                 )
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS SuspiciousEngineCapacityRecords,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
                 AND ManufacturingYear IS NOT NULL
                 AND NULLIF(LTRIM(RTRIM(MakeName)), '') IS NOT NULL
                 AND NULLIF(LTRIM(RTRIM(ModelName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS BasicValuationReadyRecords,

        SUM(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
                 AND ManufacturingYear IS NOT NULL
                 AND NULLIF(LTRIM(RTRIM(MakeName)), '') IS NOT NULL
                 AND NULLIF(LTRIM(RTRIM(ModelName)), '') IS NOT NULL
                 AND Mileage IS NOT NULL
                 AND Mileage >= 0
                 AND NULLIF(LTRIM(RTRIM(CityName)), '') IS NOT NULL
            THEN CAST(1 AS BIGINT)
            ELSE CAST(0 AS BIGINT)
        END) AS DetailedValuationReadyRecords,

        COUNT(DISTINCT NULLIF(LTRIM(RTRIM(MakeName)), ''))
            AS DistinctMakes,

        COUNT(DISTINCT NULLIF(LTRIM(RTRIM(ModelName)), ''))
            AS DistinctModels,

        COUNT(DISTINCT NULLIF(LTRIM(RTRIM(VariantName)), ''))
            AS DistinctVariants,

        COUNT(DISTINCT NULLIF(LTRIM(RTRIM(RawVehicleName)), ''))
            AS DistinctVehicleNames,

        COUNT(DISTINCT NULLIF(LTRIM(RTRIM(RawLocationName)), ''))
            AS DistinctLocations,

        AVG(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
            THEN AskingPrice
        END) AS AverageAskingPrice,

        MIN(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
            THEN AskingPrice
        END) AS MinimumAskingPrice,

        MAX(CASE
            WHEN AskingPrice IS NOT NULL
                 AND AskingPrice > 0
            THEN AskingPrice
        END) AS MaximumAskingPrice,

        MIN(ManufacturingYear) AS MinimumYear,

        MAX(ManufacturingYear) AS MaximumYear,

        MIN(Mileage) AS MinimumMileage,

        MAX(Mileage) AS MaximumMileage,

        MIN(EngineCapacity) AS MinimumEngineCapacity,

        MAX(EngineCapacity) AS MaximumEngineCapacity

    FROM dbo.UsedCarDatasetRecords
    WHERE SourceId = @SourceId
    """;

        var sourceParameter =
            new SqlParameter(
                "@SourceId",
                sourceId);

        var statistics =
            await _context.Database
                .SqlQueryRaw<UsedCarDatasetQualityRow>(
                    sql,
                    sourceParameter)
                .SingleAsync(
                    cancellationToken);

        if (statistics.TotalRecords == 0)
        {
            return new UsedCarDatasetQualityDto
            {
                SourceId = sourceId
            };
        }

        var locationRecords =
            Math.Min(
                statistics.TotalRecords,
                statistics.RecordsWithCity +
                statistics.RecordsWithProvince);

        return new UsedCarDatasetQualityDto
        {
            SourceId = sourceId,

            TotalRecords =
                statistics.TotalRecords,

            RecordsWithPrice =
                statistics.RecordsWithPrice,

            RecordsWithoutPrice =
                statistics.RecordsWithoutPrice,

            RecordsWithYear =
                statistics.RecordsWithYear,

            RecordsWithoutYear =
                statistics.RecordsWithoutYear,

            RecordsWithMileage =
                statistics.RecordsWithMileage,

            RecordsWithoutMileage =
                statistics.RecordsWithoutMileage,

            RecordsWithEngineCapacity =
                statistics.RecordsWithEngineCapacity,

            RecordsWithoutEngineCapacity =
                statistics.RecordsWithoutEngineCapacity,

            RecordsWithMake =
                statistics.RecordsWithMake,

            RecordsWithoutMake =
                statistics.RecordsWithoutMake,

            RecordsWithModel =
                statistics.RecordsWithModel,

            RecordsWithoutModel =
                statistics.RecordsWithoutModel,

            RecordsWithVariant =
                statistics.RecordsWithVariant,

            RecordsWithoutVariant =
                statistics.RecordsWithoutVariant,

            RecordsWithCity =
                statistics.RecordsWithCity,

            RecordsWithoutCity =
                statistics.TotalRecords -
                statistics.RecordsWithCity,

            RecordsWithProvince =
                statistics.RecordsWithProvince,

            RecordsWithoutProvince =
                statistics.TotalRecords -
                statistics.RecordsWithProvince,

            InvalidPriceRecords =
                statistics.InvalidPriceRecords,

            SuspiciousLowPriceRecords =
                statistics.SuspiciousLowPriceRecords,

            SuspiciousHighPriceRecords =
                statistics.SuspiciousHighPriceRecords,

            InvalidMileageRecords =
                statistics.InvalidMileageRecords,

            SuspiciousHighMileageRecords =
                statistics.SuspiciousHighMileageRecords,

            InvalidYearRecords =
                statistics.InvalidYearRecords,

            InvalidEngineCapacityRecords =
                statistics.InvalidEngineCapacityRecords,

            SuspiciousEngineCapacityRecords =
                statistics.SuspiciousEngineCapacityRecords,

            DistinctMakes =
                statistics.DistinctMakes,

            DistinctModels =
                statistics.DistinctModels,

            DistinctVariants =
                statistics.DistinctVariants,

            DistinctVehicleNames =
                statistics.DistinctVehicleNames,

            DistinctLocations =
                statistics.DistinctLocations,

            BasicValuationReadyRecords =
                statistics.BasicValuationReadyRecords,

            DetailedValuationReadyRecords =
                statistics.DetailedValuationReadyRecords,

            AverageAskingPrice =
                statistics.AverageAskingPrice,

            MinimumAskingPrice =
                statistics.MinimumAskingPrice,

            MaximumAskingPrice =
                statistics.MaximumAskingPrice,

            MinimumYear =
                statistics.MinimumYear,

            MaximumYear =
                statistics.MaximumYear,

            MinimumMileage =
                statistics.MinimumMileage,

            MaximumMileage =
                statistics.MaximumMileage,

            MinimumEngineCapacity =
                statistics.MinimumEngineCapacity,

            MaximumEngineCapacity =
                statistics.MaximumEngineCapacity,

            PriceCoveragePercentage =
                Percentage(
                    statistics.RecordsWithPrice,
                    statistics.TotalRecords),

            YearCoveragePercentage =
                Percentage(
                    statistics.RecordsWithYear,
                    statistics.TotalRecords),

            MileageCoveragePercentage =
                Percentage(
                    statistics.RecordsWithMileage,
                    statistics.TotalRecords),

            MakeCoveragePercentage =
                Percentage(
                    statistics.RecordsWithMake,
                    statistics.TotalRecords),

            ModelCoveragePercentage =
                Percentage(
                    statistics.RecordsWithModel,
                    statistics.TotalRecords),

            LocationCoveragePercentage =
                Percentage(
                    locationRecords,
                    statistics.TotalRecords),

            BasicValuationReadinessPercentage =
                Percentage(
                    statistics.BasicValuationReadyRecords,
                    statistics.TotalRecords),

            DetailedValuationReadinessPercentage =
                Percentage(
                    statistics.DetailedValuationReadyRecords,
                    statistics.TotalRecords)
        };
    }

    private static decimal Percentage(
        long value,
        long total)
    {
        if (total <= 0)
        {
            return 0;
        }

        return Math.Round(
            value * 100m / total,
            2);
    }

    private sealed class UsedCarDatasetQualityRow
    {
        public long TotalRecords { get; set; }

        public long RecordsWithPrice { get; set; }

        public long RecordsWithoutPrice { get; set; }

        public long RecordsWithYear { get; set; }

        public long RecordsWithoutYear { get; set; }

        public long RecordsWithMileage { get; set; }

        public long RecordsWithoutMileage { get; set; }

        public long RecordsWithEngineCapacity { get; set; }

        public long RecordsWithoutEngineCapacity { get; set; }

        public long RecordsWithMake { get; set; }

        public long RecordsWithoutMake { get; set; }

        public long RecordsWithModel { get; set; }

        public long RecordsWithoutModel { get; set; }

        public long RecordsWithVariant { get; set; }

        public long RecordsWithoutVariant { get; set; }

        public long RecordsWithCity { get; set; }

        public long RecordsWithProvince { get; set; }

        public long InvalidPriceRecords { get; set; }

        public long SuspiciousLowPriceRecords { get; set; }

        public long SuspiciousHighPriceRecords { get; set; }

        public long InvalidMileageRecords { get; set; }

        public long SuspiciousHighMileageRecords { get; set; }

        public long InvalidYearRecords { get; set; }

        public long InvalidEngineCapacityRecords { get; set; }

        public long SuspiciousEngineCapacityRecords { get; set; }

        public long BasicValuationReadyRecords { get; set; }

        public long DetailedValuationReadyRecords { get; set; }

        public int DistinctMakes { get; set; }

        public int DistinctModels { get; set; }

        public int DistinctVariants { get; set; }

        public int DistinctVehicleNames { get; set; }

        public int DistinctLocations { get; set; }

        public decimal? AverageAskingPrice { get; set; }

        public decimal? MinimumAskingPrice { get; set; }

        public decimal? MaximumAskingPrice { get; set; }

        public int? MinimumYear { get; set; }

        public int? MaximumYear { get; set; }

        public int? MinimumMileage { get; set; }

        public int? MaximumMileage { get; set; }

        public int? MinimumEngineCapacity { get; set; }

        public int? MaximumEngineCapacity { get; set; }
    }
}