using System.Globalization;
using System.Text;
using System.Text.Json;
using AutomotiveIntelligence.Application.DTOs.UsedCarDataset;
using AutomotiveIntelligence.Application.Services;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class UsedCarDatasetImportService
    : IUsedCarDatasetImportService
{
    private const int BatchSize = 500;

    private readonly AutomotiveIntelligenceDbContext _context;

    public UsedCarDatasetImportService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<UsedCarDatasetImportResultDto> ImportAsync(
        Stream csvStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        if (csvStream == null)
        {
            throw new ArgumentNullException(nameof(csvStream));
        }

        if (!csvStream.CanRead)
        {
            throw new InvalidOperationException(
                "The supplied stream cannot be read.");
        }

        var source = await _context.DataSources
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.SourceId == sourceId &&
                     x.IsActive,
                cancellationToken);

        if (source == null)
        {
            throw new InvalidOperationException(
                $"Active data source {sourceId} was not found.");
        }

        if (csvStream.CanSeek)
        {
            csvStream.Position = 0;
        }

        var import = new DataImport
        {
            SourceId = sourceId,
            FileName = fileName,
            ImportStatus = "Processing",
            StartedDate = DateTime.UtcNow
        };

        _context.DataImports.Add(import);

        await _context.SaveChangesAsync(
            cancellationToken);

        var importId = import.ImportId;

        var result = new UsedCarDatasetImportResultDto
        {
            ImportId = importId
        };

        try
        {
            using var reader = new StreamReader(
                csvStream,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                leaveOpen: true);

            var headerLine =
                await reader.ReadLineAsync(
                    cancellationToken);

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new InvalidOperationException(
                    "The CSV file does not contain a header.");
            }

            var headers = ParseCsvLine(headerLine);

            ValidateHeaders(headers);

            var existingExternalIds =
                await _context.UsedCarDatasetRecords
                    .AsNoTracking()
                    .Where(x =>
                        x.SourceId == sourceId &&
                        x.ExternalRecordId != null)
                    .Select(x => x.ExternalRecordId!)
                    .ToHashSetAsync(
                        StringComparer.OrdinalIgnoreCase,
                        cancellationToken);

            var batch =
                new List<UsedCarDatasetRecord>(
                    BatchSize);

            string? line;

            var rowNumber = 1;

            while ((line =
                await reader.ReadLineAsync(
                    cancellationToken)) != null)
            {
                rowNumber++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                result.TotalRows++;

                try
                {
                    var values =
                        ParseCsvLine(line);

                    var row =
                        CreateRow(
                            headers,
                            values);

                    var externalId =
                        GetValue(
                            row,
                            "Ad Reference");

                    if (string.IsNullOrWhiteSpace(
                            externalId))
                    {
                        result.RejectedRows++;

                        result.Errors.Add(
                            new UsedCarDatasetImportErrorDto
                            {
                                RowNumber = rowNumber,
                                ErrorType = "Validation",
                                Error =
                                    "Ad Reference is required."
                            });

                        continue;
                    }

                    if (existingExternalIds.Contains(
                            externalId))
                    {
                        result.DuplicateRows++;
                        continue;
                    }

                    var record =
                        MapRecord(
                            row,
                            sourceId,
                            importId);

                    batch.Add(record);

                    existingExternalIds.Add(
                        externalId);

                    if (batch.Count >= BatchSize)
                    {
                        await SaveBatchAsync(
                            batch,
                            cancellationToken);

                        result.ImportedRows +=
                            batch.Count;

                        batch.Clear();
                    }
                }
                catch (Exception ex)
                {
                    result.RejectedRows++;

                    result.Errors.Add(
                        new UsedCarDatasetImportErrorDto
                        {
                            RowNumber = rowNumber,
                            ErrorType = "Validation",
                            Error = ex.Message
                        });
                }
            }

            if (batch.Count > 0)
            {
                await SaveBatchAsync(
                    batch,
                    cancellationToken);

                result.ImportedRows +=
                    batch.Count;

                batch.Clear();
            }

            await CompleteImportAsync(
                importId,
                result,
                cancellationToken);

            result.Success = true;

            return result;
        }
        catch (Exception ex)
        {
            await FailImportAsync(
                importId,
                ex,
                cancellationToken);

            result.Success = false;

            throw;
        }
    }

    private async Task SaveBatchAsync(
        List<UsedCarDatasetRecord> batch,
        CancellationToken cancellationToken)
    {
        if (batch.Count == 0)
        {
            return;
        }

        _context.UsedCarDatasetRecords.AddRange(
            batch);

        await _context.SaveChangesAsync(
            cancellationToken);

        /*
         * Critical for large imports:
         * detach all entities from EF Core's
         * change tracker after every batch.
         */
        _context.ChangeTracker.Clear();
    }

    private async Task CompleteImportAsync(
        long importId,
        UsedCarDatasetImportResultDto result,
        CancellationToken cancellationToken)
    {
        var import = new DataImport
        {
            ImportId = importId,
            RecordsProcessed =
                result.TotalRows,
            RecordsInserted =
                result.ImportedRows,
            RecordsRejected =
                result.RejectedRows,
            RecordsDuplicated =
                result.DuplicateRows,
            ImportStatus =
                result.RejectedRows > 0
                    ? "CompletedWithErrors"
                    : "Completed",
            CompletedDate =
                DateTime.UtcNow,
            ErrorLog =
                result.Errors.Count == 0
                    ? null
                    : JsonSerializer.Serialize(
                        result.Errors)
        };

        _context.DataImports.Attach(import);

        _context.Entry(import)
            .Property(x => x.RecordsProcessed)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.RecordsInserted)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.RecordsRejected)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.RecordsDuplicated)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.ImportStatus)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.CompletedDate)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.ErrorLog)
            .IsModified = true;

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    private async Task FailImportAsync(
        long importId,
        Exception exception,
        CancellationToken cancellationToken)
    {
        /*
         * The ChangeTracker may contain entities from
         * the failed batch, so clear it before updating
         * the DataImport record.
         */
        _context.ChangeTracker.Clear();

        var import = new DataImport
        {
            ImportId = importId,
            ImportStatus = "Failed",
            CompletedDate = DateTime.UtcNow,
            ErrorLog = exception.ToString()
        };

        _context.DataImports.Attach(import);

        _context.Entry(import)
            .Property(x => x.ImportStatus)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.CompletedDate)
            .IsModified = true;

        _context.Entry(import)
            .Property(x => x.ErrorLog)
            .IsModified = true;

        await _context.SaveChangesAsync(
            cancellationToken);
    }

    private static void ValidateHeaders(
        List<string> headers)
    {
        var requiredHeaders = new[]
        {
            "nam",
            "Price",
            "Year",
            "Millage",
            "Fuel",
            "Transmission",
            "Province",
            "Engine Capacity",
            "Ad Reference",
            "url"
        };

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!headers.Any(
                    x => string.Equals(
                        x,
                        requiredHeader,
                        StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    $"Required column '{requiredHeader}' was not found.");
            }
        }
    }

    private static UsedCarDatasetRecord MapRecord(
        Dictionary<string, string?> row,
        int sourceId,
        long importId)
    {
        var rawVehicleName =
            GetValue(row, "nam");

        var location =
            GetValue(row, "Province");

        var parsedVehicle =
            ParseVehicleName(rawVehicleName);

        return new UsedCarDatasetRecord
        {
            SourceId = sourceId,
            ImportId = importId,

            ExternalRecordId =
                GetValue(row, "Ad Reference"),

            RawVehicleName =
                rawVehicleName,

            MakeName =
                parsedVehicle.Make,

            ModelName =
                parsedVehicle.Model,

            VariantName =
                parsedVehicle.Variant,

            ManufacturingYear =
                ParseNullableInt(
                    GetValue(row, "Year")),

            RawLocationName =
                location,

            ProvinceName =
                ResolveProvince(location),

            CityName =
                ResolveCity(location),

            Mileage =
                ParseMileage(
                    GetValue(row, "Millage")),

            EngineCapacity =
                ParseEngineCapacity(
                    GetValue(row, "Engine Capacity")),

            Transmission =
                NormalizeText(
                    GetValue(row, "Transmission")),

            FuelType =
                NormalizeText(
                    GetValue(row, "Fuel")),

            AskingPrice =
                ParsePrice(
                    GetValue(row, "Price")),

            Color =
                NormalizeText(
                    GetValue(row, "Color")),

            AssemblyType =
                NormalizeText(
                    GetValue(row, "Assembly")),

            BodyType =
                NormalizeText(
                    GetValue(row, "Body Type")),

            Features =
                NormalizeText(
                    GetValue(row, "Features")),

            SellerName =
                NormalizeText(
                    GetValue(row, "Owner nam")),

            SourceUrl =
                NormalizeText(
                    GetValue(row, "url")),

            ImportedDate =
                DateTime.UtcNow
        };
    }

    private static (
        string? Make,
        string? Model,
        string? Variant)
        ParseVehicleName(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return (null, null, null);
        }

        var parts =
            value.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return (
                parts[0],
                null,
                null);
        }

        var knownMakes = new[]
        {
            "Toyota",
            "Honda",
            "Suzuki",
            "KIA",
            "Kia",
            "Hyundai",
            "Nissan",
            "Daihatsu",
            "Mitsubishi",
            "BMW",
            "Mercedes",
            "Mercedes-Benz",
            "Audi",
            "Lexus",
            "Mazda",
            "Ford",
            "Chevrolet",
            "Changan",
            "MG",
            "Proton",
            "FAW",
            "Isuzu"
        };

        var make =
            knownMakes.FirstOrDefault(
                x => string.Equals(
                    x,
                    parts[0],
                    StringComparison.OrdinalIgnoreCase));

        if (make == null)
        {
            return (
                parts[0],
                parts.Length > 1
                    ? parts[1]
                    : null,
                parts.Length > 2
                    ? string.Join(
                        " ",
                        parts.Skip(2))
                    : null);
        }

        var model =
            parts.Length > 1
                ? parts[1]
                : null;

        var variant =
            parts.Length > 2
                ? string.Join(
                    " ",
                    parts.Skip(2))
                : null;

        return (
            make,
            model,
            variant);
    }

    private static decimal? ParsePrice(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var normalized =
            value
                .Trim()
                .ToLowerInvariant()
                .Replace("pkr", "")
                .Replace(",", "")
                .Trim();

        decimal multiplier = 1;

        if (normalized.Contains("crore") ||
            normalized.Contains("crores") ||
            normalized.Contains("cr"))
        {
            multiplier = 10_000_000;
        }
        else if (normalized.Contains("lac") ||
                 normalized.Contains("lacs") ||
                 normalized.Contains("lakh"))
        {
            multiplier = 100_000;
        }
        else if (normalized.EndsWith("k"))
        {
            multiplier = 1_000;
        }

        normalized =
            normalized
                .Replace("crores", "")
                .Replace("crore", "")
                .Replace("lacs", "")
                .Replace("lac", "")
                .Replace("lakh", "")
                .Replace("cr", "")
                .Replace("k", "")
                .Trim();

        if (!decimal.TryParse(
                normalized,
                NumberStyles.Number |
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out var amount))
        {
            return null;
        }

        return amount * multiplier;
    }

    private static int? ParseMileage(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var digits =
            new string(
                value.Where(
                    char.IsDigit)
                .ToArray());

        return int.TryParse(
            digits,
            out var result)
            ? result
            : null;
    }

    private static int? ParseEngineCapacity(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var digits =
            new string(
                value.Where(
                    char.IsDigit)
                .ToArray());

        return int.TryParse(
            digits,
            out var result)
            ? result
            : null;
    }

    private static int? ParseNullableInt(
        string? value)
    {
        return int.TryParse(
            value,
            NumberStyles.Integer,
            CultureInfo.InvariantCulture,
            out var result)
            ? result
            : null;
    }

    private static string? NormalizeText(
        string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var normalized =
            value.Trim();

        if (string.Equals(
                normalized,
                "N/A",
                StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        return normalized;
    }

    private static string? ResolveProvince(
        string? location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return null;
        }

        var value =
            location.Trim();

        var provinces =
            new HashSet<string>(
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

        return provinces.Contains(value)
            ? value
            : null;
    }

    private static string? ResolveCity(
        string? location)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return null;
        }

        var value =
            location.Trim();

        var knownCities =
            new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
            {
                "Lahore",
                "Rawalpindi",
                "Faisalabad",
                "Multan",
                "Gujranwala",
                "Karachi",
                "Hyderabad",
                "Peshawar",
                "Abbottabad",
                "Quetta",
                "Islamabad"
            };

        return knownCities.Contains(value)
            ? value
            : null;
    }

    private static Dictionary<string, string?> CreateRow(
        List<string> headers,
        List<string> values)
    {
        var row =
            new Dictionary<string, string?>(
                StringComparer.OrdinalIgnoreCase);

        for (var i = 0;
             i < headers.Count;
             i++)
        {
            row[headers[i]] =
                i < values.Count
                    ? NormalizeText(values[i])
                    : null;
        }

        return row;
    }

    private static string? GetValue(
        Dictionary<string, string?> row,
        string column)
    {
        return row.TryGetValue(
            column,
            out var value)
            ? value
            : null;
    }

    private static List<string> ParseCsvLine(
        string line)
    {
        var result = new List<string>();

        var current =
            new StringBuilder();

        var insideQuotes = false;

        for (var i = 0;
             i < line.Length;
             i++)
        {
            var character = line[i];

            if (character == '"')
            {
                if (insideQuotes &&
                    i + 1 < line.Length &&
                    line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;

                    continue;
                }

                insideQuotes =
                    !insideQuotes;

                continue;
            }

            if (character == ',' &&
                !insideQuotes)
            {
                result.Add(
                    current.ToString());

                current.Clear();

                continue;
            }

            current.Append(character);
        }

        result.Add(
            current.ToString());

        return result;
    }
}