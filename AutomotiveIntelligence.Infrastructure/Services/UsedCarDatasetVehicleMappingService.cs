using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class UsedCarDatasetVehicleMappingService
    : IUsedCarDatasetVehicleMappingService
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public UsedCarDatasetVehicleMappingService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<UsedCarDatasetVehicleMappingResultDto> MapAsync(
        int sourceId,
        int batchSize = 500,
        CancellationToken cancellationToken = default)
    {
        if (batchSize <= 0)
        {
            batchSize = 500;
        }

        var result = new UsedCarDatasetVehicleMappingResultDto
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

            /*
             * Existing mappings make the operation resumable.
             */
            var existingMappings =
                await _context.UsedCarDatasetVehicleMappings
                    .AsNoTracking()
                    .Where(x =>
                        recordIds.Contains(
                            x.UsedCarDatasetRecordId))
                    .ToDictionaryAsync(
                        x => x.UsedCarDatasetRecordId,
                        cancellationToken);

            foreach (var record in records)
            {
                result.ProcessedRecords++;

                if (existingMappings.ContainsKey(
                        record.UsedCarDatasetRecordId))
                {
                    result.DuplicateRecords++;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(record.MakeName))
                {
                    result.PendingRecords++;
                    result.MakeNotFoundRecords++;

                    AddPendingMapping(
                        record,
                        sourceId,
                        "Make is missing.");

                    continue;
                }

                var makeName = NormalizeText(record.MakeName);

                var make = await _context.Makes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        x =>
                            x.IsActive &&
                            x.Name == makeName,
                        cancellationToken);

                if (make == null)
                {
                    result.PendingRecords++;
                    result.MakeNotFoundRecords++;

                    AddPendingMapping(
                        record,
                        sourceId,
                        $"Canonical make not found: {record.MakeName}");

                    continue;
                }

                if (string.IsNullOrWhiteSpace(record.ModelName))
                {
                    result.PendingRecords++;
                    result.ModelNotFoundRecords++;

                    AddPendingMapping(
                        record,
                        sourceId,
                        "Model is missing.",
                        make.MakeId);

                    continue;
                }

                var modelName = NormalizeText(record.ModelName);

                var model = await _context.Models
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        x =>
                            x.IsActive &&
                            x.MakeId == make.MakeId &&
                            x.Name == modelName,
                        cancellationToken);

                if (model == null)
                {
                    result.PendingRecords++;
                    result.ModelNotFoundRecords++;

                    AddPendingMapping(
                        record,
                        sourceId,
                        $"Canonical model not found: {record.ModelName}",
                        make.MakeId);

                    continue;
                }

                var variants = await _context.Variants
                    .AsNoTracking()
                    .Where(x =>
                        x.IsActive &&
                        x.ModelId == model.ModelId)
                    .ToListAsync(cancellationToken);

                /*
                 * Try exact variant first.
                 */
                if (!string.IsNullOrWhiteSpace(
                        record.VariantName))
                {
                    var variantName =
                        NormalizeText(record.VariantName);

                    var exactVariant = variants
                        .FirstOrDefault(
                            x => x.Name == variantName);

                    if (exactVariant != null)
                    {
                        AddMappedRecord(
                            record,
                            sourceId,
                            make,
                            model,
                            exactVariant,
                            "ExactVariant",
                            1.00m);

                        result.MappedRecords++;
                        result.ExactVariantMatches++;

                        continue;
                    }
                }

                /*
                 * If the canonical model has exactly one
                 * variant, mapping it is safe.
                 */
                if (variants.Count == 1)
                {
                    AddMappedRecord(
                        record,
                        sourceId,
                        make,
                        model,
                        variants[0],
                        "SingleVariantModel",
                        0.90m);

                    result.MappedRecords++;
                    result.SingleVariantModelMatches++;

                    continue;
                }

                /*
                 * Multiple canonical variants exist but
                 * source variant could not be resolved.
                 *
                 * Do not guess.
                 */
                result.PendingRecords++;
                result.VariantNotFoundRecords++;

                AddPendingMapping(
                    record,
                    sourceId,
                    $"Variant could not be safely mapped: {record.VariantName}",
                    make.MakeId,
                    model.ModelId);
            }

            await _context.SaveChangesAsync(
                cancellationToken);
        }

        var totalRecords =
            await _context.UsedCarDatasetRecords
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        var totalMappings =
            await _context.UsedCarDatasetVehicleMappings
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        result.PendingRecords = Math.Max(
            0,
            totalRecords - totalMappings);

        result.Status =
            result.PendingRecords == 0
                ? "Completed"
                : "PartiallyCompleted";

        return result;
    }

    public async Task<UsedCarDatasetVehicleMappingResultDto>
        GetStatusAsync(
            int sourceId,
            CancellationToken cancellationToken = default)
    {
        var totalRecords =
            await _context.UsedCarDatasetRecords
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        var mappings =
            await _context.UsedCarDatasetVehicleMappings
                .AsNoTracking()
                .Where(x => x.SourceId == sourceId)
                .ToListAsync(cancellationToken);

        return new UsedCarDatasetVehicleMappingResultDto
        {
            SourceId = sourceId,
            ProcessedRecords = mappings.Count,
            MappedRecords = mappings.Count(
                x => x.MappingStatus == "Mapped"),
            PendingRecords = totalRecords - mappings.Count,
            ExactVariantMatches = mappings.Count(
                x => x.MappingMethod == "ExactVariant"),
            SingleVariantModelMatches = mappings.Count(
                x => x.MappingMethod == "SingleVariantModel"),
            Status = mappings.Count == totalRecords
                ? "Completed"
                : "Pending"
        };
    }

    private void AddMappedRecord(
        UsedCarDatasetRecord record,
        int sourceId,
        Make make,
        Model model,
        Variant variant,
        string method,
        decimal confidence)
    {
        _context.UsedCarDatasetVehicleMappings.Add(
            new UsedCarDatasetVehicleMapping
            {
                UsedCarDatasetRecordId =
                    record.UsedCarDatasetRecordId,

                SourceId = sourceId,

                MakeId = make.MakeId,

                ModelId = model.ModelId,

                VariantId = variant.VariantId,

                MappingStatus = "Mapped",

                MappingMethod = method,

                ConfidenceScore = confidence,

                MappingNotes =
                    $"Mapped from source values: " +
                    $"{record.MakeName} / " +
                    $"{record.ModelName} / " +
                    $"{record.VariantName}",

                MappedDate = DateTime.UtcNow
            });
    }

    private void AddPendingMapping(
        UsedCarDatasetRecord record,
        int sourceId,
        string notes,
        int? makeId = null,
        int? modelId = null)
    {
        _context.UsedCarDatasetVehicleMappings.Add(
            new UsedCarDatasetVehicleMapping
            {
                UsedCarDatasetRecordId =
                    record.UsedCarDatasetRecordId,

                SourceId = sourceId,

                MakeId = makeId,

                ModelId = modelId,

                MappingStatus = "Pending",

                MappingMethod = "Unresolved",

                ConfidenceScore = 0m,

                MappingNotes = notes,

                MappedDate = DateTime.UtcNow
            });
    }

    private static string NormalizeText(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return string.Join(
            ' ',
            value
                .Trim()
                .Split(
                    (char[]?)null,
                    StringSplitOptions.RemoveEmptyEntries));
    }
}