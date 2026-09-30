using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class UsedCarMarketProjectionService
    : IUsedCarMarketProjectionService
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public UsedCarMarketProjectionService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<UsedCarMarketProjectionResultDto> ProjectAsync(
        int sourceId,
        int batchSize = 500,
        CancellationToken cancellationToken = default)
    {
        if (batchSize <= 0)
        {
            batchSize = 500;
        }

        var result = new UsedCarMarketProjectionResultDto
        {
            SourceId = sourceId,
            Status = "Processing"
        };

        var sourceExists = await _context.DataSources
            .AsNoTracking()
            .AnyAsync(
                x => x.SourceId == sourceId,
                cancellationToken);

        if (!sourceExists)
        {
            result.Status = "Failed";
            return result;
        }

        long lastRecordId = 0;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var batchRecordIds =
                await _context.UsedCarDatasetRecords
                    .AsNoTracking()
                    .Where(x =>
                        x.SourceId == sourceId &&
                        x.UsedCarDatasetRecordId > lastRecordId)
                    .OrderBy(x => x.UsedCarDatasetRecordId)
                    .Select(x => x.UsedCarDatasetRecordId)
                    .Take(batchSize)
                    .ToListAsync(cancellationToken);

            if (batchRecordIds.Count == 0)
            {
                break;
            }

            lastRecordId = batchRecordIds[^1];

            var records =
                await _context.UsedCarDatasetRecords
                    .AsNoTracking()
                    .Where(x =>
                        batchRecordIds.Contains(
                            x.UsedCarDatasetRecordId))
                    .OrderBy(x => x.UsedCarDatasetRecordId)
                    .ToListAsync(cancellationToken);

            var recordIds = records
                .Select(x => x.UsedCarDatasetRecordId)
                .ToList();

            var normalizations =
                await _context.UsedCarDatasetNormalizations
                    .AsNoTracking()
                    .Where(x =>
                        recordIds.Contains(
                            x.UsedCarDatasetRecordId))
                    .ToDictionaryAsync(
                        x => x.UsedCarDatasetRecordId,
                        cancellationToken);

            /*
             * Use the vehicle mapping layer rather than
             * attempting Make/Model/Variant resolution again.
             */
            var mappings =
                await _context.UsedCarDatasetVehicleMappings
                    .AsNoTracking()
                    .Where(x =>
                        recordIds.Contains(
                            x.UsedCarDatasetRecordId) &&
                        x.MappingStatus == "Mapped" &&
                        x.VariantId.HasValue)
                    .ToDictionaryAsync(
                        x => x.UsedCarDatasetRecordId,
                        cancellationToken);

            /*
             * Find MarketPrice records that already came
             * from this source record.
             */
            var existingSourceRecordIds =
                await _context.MarketPrices
                    .AsNoTracking()
                    .Where(x =>
                        x.SourceId == sourceId &&
                        x.SourceRecordId.HasValue &&
                        recordIds.Contains(
                            x.SourceRecordId.Value))
                    .Select(x => x.SourceRecordId!.Value)
                    .ToHashSetAsync(cancellationToken);

            foreach (var record in records)
            {
                result.ProcessedRecords++;

                if (existingSourceRecordIds.Contains(
                        record.UsedCarDatasetRecordId))
                {
                    result.DuplicateRecords++;
                    continue;
                }

                if (!normalizations.TryGetValue(
                        record.UsedCarDatasetRecordId,
                        out var normalization))
                {
                    result.SkippedRecords++;
                    continue;
                }

                /*
                 * Only city-level observations are suitable
                 * for our current precise MarketPrice layer.
                 */
                if (!normalization.CityId.HasValue ||
                    normalization.LocationType != "City")
                {
                    result.SkippedRecords++;

                    if (normalization.LocationType == "Province")
                    {
                        result.ProvinceLocationRecords++;
                    }
                    else
                    {
                        result.UnknownLocationRecords++;
                    }

                    continue;
                }

                /*
                 * Invalid years are excluded from valuation
                 * observations.
                 */
                if (!record.ManufacturingYear.HasValue ||
                    normalization.YearStatus != "Valid")
                {
                    result.InvalidYearRecords++;
                    result.SkippedRecords++;
                    continue;
                }

                /*
                 * Missing/non-positive prices cannot become
                 * MarketPrice observations.
                 */
                if (!record.AskingPrice.HasValue ||
                    record.AskingPrice.Value <= 0 ||
                    normalization.PriceStatus == "Missing")
                {
                    result.MissingPriceRecords++;
                    result.SkippedRecords++;
                    continue;
                }

                /*
                 * Vehicle mapping must have successfully
                 * identified a canonical Variant.
                 */
                if (!mappings.TryGetValue(
                        record.UsedCarDatasetRecordId,
                        out var mapping) ||
                    !mapping.VariantId.HasValue)
                {
                    result.SkippedRecords++;
                    continue;
                }

                var marketPrice = new MarketPrice
                {
                    VariantId = mapping.VariantId.Value,

                    Year = record.ManufacturingYear.Value,

                    CityId = normalization.CityId.Value,

                    ConditionId = null,

                    Mileage = record.Mileage,

                    ObservedPrice = record.AskingPrice.Value,

                    SourceId = sourceId,

                    SourceRecordId =
                        record.UsedCarDatasetRecordId,

                    ObservationDate =
                        record.ListingDate ??
                        record.ImportedDate,

                    CreatedDate = DateTime.UtcNow
                };

                _context.MarketPrices.Add(marketPrice);

                existingSourceRecordIds.Add(
                    record.UsedCarDatasetRecordId);

                result.ProjectedRecords++;

                result.MarketPriceRecordsCreated++;

                result.CityLocationRecords++;
            }

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        var totalSourceRecords =
            await _context.UsedCarDatasetRecords
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        var projectedSourceRecords =
            await _context.MarketPrices
                .AsNoTracking()
                .CountAsync(
                    x =>
                        x.SourceId == sourceId &&
                        x.SourceRecordId.HasValue,
                    cancellationToken);

        result.PendingRecords = Math.Max(
            0,
            totalSourceRecords -
            projectedSourceRecords);

        result.Status =
            result.PendingRecords == 0
                ? "Completed"
                : "PartiallyCompleted";

        return result;
    }

    public async Task<UsedCarMarketProjectionResultDto>
        GetStatusAsync(
            int sourceId,
            CancellationToken cancellationToken = default)
    {
        var totalSourceRecords =
            await _context.UsedCarDatasetRecords
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        var projectedRecords =
            await _context.MarketPrices
                .AsNoTracking()
                .CountAsync(
                    x =>
                        x.SourceId == sourceId &&
                        x.SourceRecordId.HasValue,
                    cancellationToken);

        var pendingRecords = Math.Max(
            0,
            totalSourceRecords -
            projectedRecords);

        return new UsedCarMarketProjectionResultDto
        {
            SourceId = sourceId,

            ProcessedRecords = projectedRecords,

            ProjectedRecords = projectedRecords,

            MarketPriceRecordsCreated =
                projectedRecords,

            PendingRecords = pendingRecords,

            Status = pendingRecords == 0
                ? "Completed"
                : "Pending"
        };
    }
}