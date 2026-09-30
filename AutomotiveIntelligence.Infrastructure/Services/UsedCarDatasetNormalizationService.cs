using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class UsedCarDatasetNormalizationService
    : IUsedCarDatasetNormalizationService
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public UsedCarDatasetNormalizationService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<UsedCarDatasetNormalizationResultDto>
        NormalizeAsync(
            int sourceId,
            int batchSize = 500,
            CancellationToken cancellationToken = default)
    {
        if (batchSize < 100 || batchSize > 5000)
        {
            batchSize = 1000;
        }

        var result = new UsedCarDatasetNormalizationResultDto
        {
            SourceId = sourceId,
            Status = "Processing"
        };

        long lastRecordId = 0;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var batchRecordIds = await _context.UsedCarDatasetRecords
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

            var records = await _context.UsedCarDatasetRecords
                .Where(x => batchRecordIds.Contains(x.UsedCarDatasetRecordId))
                .OrderBy(x => x.UsedCarDatasetRecordId)
                .ToListAsync(cancellationToken);

            if (records.Count == 0)
            {
                break;
            }

            lastRecordId =
                records[^1].UsedCarDatasetRecordId;

            var recordIds = records
                .Select(x => x.UsedCarDatasetRecordId)
                .ToList();

            var existingIds = await _context
                .UsedCarDatasetNormalizations
                .AsNoTracking()
                .Where(x =>
                    recordIds.Contains(
                        x.UsedCarDatasetRecordId))
                .Select(x =>
                    x.UsedCarDatasetRecordId)
                .ToHashSetAsync(cancellationToken);

            foreach (var record in records)
            {
                if (existingIds.Contains(
                    record.UsedCarDatasetRecordId))
                {
                    continue;
                }

                var normalization =
                    NormalizeRecord(record);

                _context.UsedCarDatasetNormalizations
                    .Add(normalization);

                result.ProcessedRecords++;

                if (normalization.NormalizationStatus
                    == "Normalized")
                {
                    result.NormalizedRecords++;
                }

                if (normalization.YearStatus == "Invalid")
                {
                    result.InvalidYearRecords++;
                }

                if (normalization.PriceStatus == "Missing")
                {
                    result.MissingPriceRecords++;
                }

                if (normalization.PriceStatus == "High"
                    || normalization.PriceStatus == "Extreme")
                {
                    result.HighPriceRecords++;
                }

                switch (normalization.LocationType)
                {
                    case "City":
                        result.CityRecords++;
                        break;

                    case "Province":
                        result.ProvinceRecords++;
                        break;

                    case "Unregistered":
                        result.UnregisteredRecords++;
                        break;

                    case "Unknown":
                        result.UnknownLocationRecords++;
                        break;
                }

                if (normalization.IsBasicValuationReady)
                {
                    result.BasicValuationReadyRecords++;
                }

                if (normalization.IsDetailedValuationReady)
                {
                    result.DetailedValuationReadyRecords++;
                }
            }

            await _context.SaveChangesAsync(
                cancellationToken);

            _context.ChangeTracker.Clear();
        }

        var totalSourceRecords =
     await _context.UsedCarDatasetRecords
         .AsNoTracking()
         .CountAsync(
             x => x.SourceId == sourceId,
             cancellationToken);

        var normalizedSourceRecords =
            await _context.UsedCarDatasetNormalizations
                .AsNoTracking()
                .CountAsync(
                    x => x.SourceId == sourceId,
                    cancellationToken);

        result.PendingRecords =
            Math.Max(
                0,
                totalSourceRecords -
                normalizedSourceRecords);

        result.Status =
            result.PendingRecords == 0
                ? "Completed"
                : "PartiallyCompleted";

        return result;
    }

    public async Task<UsedCarDatasetNormalizationResultDto>
        GetStatusAsync(
            int sourceId,
            CancellationToken cancellationToken = default)
    {
        var query = _context
            .UsedCarDatasetNormalizations
            .AsNoTracking()
            .Where(x => x.SourceId == sourceId);

        var total = await query
            .CountAsync(cancellationToken);

        var normalized = await query
            .CountAsync(
                x => x.NormalizationStatus == "Normalized",
                cancellationToken);

        var invalidYears = await query
            .CountAsync(
                x => x.YearStatus == "Invalid",
                cancellationToken);

        var missingPrices = await query
            .CountAsync(
                x => x.PriceStatus == "Missing",
                cancellationToken);

        var highPrices = await query
            .CountAsync(
                x => x.PriceStatus == "High"
                    || x.PriceStatus == "Extreme",
                cancellationToken);

        var cityRecords = await query
            .CountAsync(
                x => x.LocationType == "City",
                cancellationToken);

        var provinceRecords = await query
            .CountAsync(
                x => x.LocationType == "Province",
                cancellationToken);

        var unregisteredRecords = await query
            .CountAsync(
                x => x.LocationType == "Unregistered",
                cancellationToken);

        var unknownLocations = await query
            .CountAsync(
                x => x.LocationType == "Unknown",
                cancellationToken);

        var basicReady = await query
            .CountAsync(
                x => x.IsBasicValuationReady,
                cancellationToken);

        var detailedReady = await query
            .CountAsync(
                x => x.IsDetailedValuationReady,
                cancellationToken);

        return new UsedCarDatasetNormalizationResultDto
        {
            SourceId = sourceId,

            ProcessedRecords = total,

            NormalizedRecords = normalized,

            InvalidYearRecords = invalidYears,

            MissingPriceRecords = missingPrices,

            HighPriceRecords = highPrices,

            CityRecords = cityRecords,

            ProvinceRecords = provinceRecords,

            UnregisteredRecords =
                unregisteredRecords,

            UnknownLocationRecords =
                unknownLocations,

            BasicValuationReadyRecords =
                basicReady,

            DetailedValuationReadyRecords =
                detailedReady,

            Status = total > 0
                ? "Completed"
                : "NotStarted"
        };
    }

    private static UsedCarDatasetNormalization
        NormalizeRecord(
            UsedCarDatasetRecord record)
    {
        var normalizedMake =
            NormalizeText(record.MakeName);

        var normalizedModel =
            NormalizeText(record.ModelName);

        var normalizedVariant =
            NormalizeText(record.VariantName);

        var location =
            NormalizeLocation(record.CityName);

        var yearStatus =
            GetYearStatus(
                record.ManufacturingYear);

        var priceStatus =
            GetPriceStatus(
                record.AskingPrice);

        var basicReady =
            record.AskingPrice.HasValue
            && record.AskingPrice.Value > 0
            && record.ManufacturingYear.HasValue
            && record.ManufacturingYear.Value >= 1950
            && record.ManufacturingYear.Value
                <= DateTime.UtcNow.Year + 1
            && !string.IsNullOrWhiteSpace(
                normalizedMake)
            && !string.IsNullOrWhiteSpace(
                normalizedModel);

        var detailedReady =
            basicReady
            && record.Mileage.HasValue
            && location.Type == "City";

        var notes = new List<string>();

        if (yearStatus == "Invalid")
        {
            notes.Add(
                "Manufacturing year requires review.");
        }

        if (priceStatus == "Missing")
        {
            notes.Add(
                "Asking price is missing.");
        }

        if (priceStatus == "High"
            || priceStatus == "Extreme")
        {
            notes.Add(
                "Price is an outlier candidate; "
                + "not automatically invalid.");
        }

        if (location.Type == "Unknown")
        {
            notes.Add(
                "Location requires mapping.");
        }

        return new UsedCarDatasetNormalization
        {
            UsedCarDatasetRecordId =
                record.UsedCarDatasetRecordId,

            SourceId =
                record.SourceId,

            NormalizationStatus =
                "Normalized",

            NormalizedMakeName =
                normalizedMake,

            NormalizedModelName =
                normalizedModel,

            NormalizedVariantName =
                normalizedVariant,

            NormalizedLocationName =
                location.Name,

            LocationType =
                location.Type,

            YearStatus =
                yearStatus,

            PriceStatus =
                priceStatus,

            IsBasicValuationReady =
                basicReady,

            IsDetailedValuationReady =
                detailedReady,

            NormalizationNotes =
                notes.Count == 0
                    ? null
                    : string.Join(
                        " ",
                        notes),

            NormalizedDate =
                DateTime.UtcNow
        };
    }

    private static string? NormalizeText(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var parts = value
            .Trim()
            .Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

        return string.Join(" ", parts);
    }

    private static string GetYearStatus(
        int? year)
    {
        if (!year.HasValue)
        {
            return "Missing";
        }

        return year.Value >= 1950
               && year.Value <= DateTime.UtcNow.Year + 1
            ? "Valid"
            : "Invalid";
    }

    private static string GetPriceStatus(
        decimal? price)
    {
        if (!price.HasValue || price.Value <= 0)
        {
            return "Missing";
        }

        if (price.Value >= 200_000_000m)
        {
            return "Extreme";
        }

        if (price.Value >= 100_000_000m)
        {
            return "High";
        }

        return "Normal";
    }

    private static (
        string? Name,
        string Type)
        NormalizeLocation(
            string? location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return (null, "Unknown");
        }

        var value = location.Trim();

        if (value.Equals(
                "Un-Registered",
                StringComparison.OrdinalIgnoreCase)
            || value.Equals(
                "Unregistered",
                StringComparison.OrdinalIgnoreCase))
        {
            return (
                value,
                "Unregistered");
        }

        var provinces = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            "Punjab",
            "Sindh",
            "Khyber Pakhtunkhwa",
            "KPK",
            "Balochistan",
            "Islamabad Capital Territory",
            "ICT"
        };

        if (provinces.Contains(value))
        {
            return (
                value,
                "Province");
        }

        var knownCities = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase)
        {
            "Lahore",
            "Rawalpindi",
            "Islamabad",
            "Faisalabad",
            "Multan",
            "Gujranwala",
            "Karachi",
            "Hyderabad",
            "Peshawar",
            "Abbottabad",
            "Quetta"
        };

        if (knownCities.Contains(value))
        {
            return (
                value,
                "City");
        }

        return (
            value,
            "Unknown");
    }
}