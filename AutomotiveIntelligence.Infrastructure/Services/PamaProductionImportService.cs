using System.Globalization;
using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class PamaProductionImportService
    : IPamaProductionImportService
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public PamaProductionImportService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<PamaProductionImportResultDto> ImportAsync(
        Stream dataStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        var result = new PamaProductionImportResultDto();

        var source = await _context.DataSources
            .FirstOrDefaultAsync(
                x => x.SourceId == sourceId &&
                     x.IsActive,
                cancellationToken);

        if (source == null)
        {
            result.Success = false;
            result.Errors.Add(new PamaProductionImportErrorDto
            {
                RowNumber = 0,
                ErrorType = "Source",
                Error = "The specified data source was not found or is inactive."
            });

            return result;
        }

        var import = new DataImport
        {
            SourceId = sourceId,
            FileName = fileName,
            RecordsProcessed = 0,
            RecordsInserted = 0,
            RecordsRejected = 0,
            RecordsDuplicated = 0,
            ImportStatus = "Processing",
            StartedDate = DateTime.UtcNow
        };

        _context.DataImports.Add(import);
        await _context.SaveChangesAsync(cancellationToken);

        try
        {
            using var reader = new StreamReader(dataStream);

            var header = await reader.ReadLineAsync(
                cancellationToken);

            if (string.IsNullOrWhiteSpace(header))
            {
                result.Errors.Add(new PamaProductionImportErrorDto
                {
                    RowNumber = 1,
                    ErrorType = "Header",
                    Error = "The CSV file is empty or does not contain a header."
                });

                result.FailedRows = 1;
                result.Success = false;

                await CompleteImportAsync(
                    import,
                    result,
                    cancellationToken);

                return result;
            }

            var columns = ParseCsvLine(header);

            var requiredColumns = new[]
            {
                "ManufacturerName",
                "VehicleType",
                "Year",
                "Month",
                "ProductionUnits",
                "SalesUnits",
                "ObservationDate"
            };

            var columnIndexes = new Dictionary<string, int>(
                StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < columns.Count; i++)
            {
                var column = columns[i].Trim();

                if (!columnIndexes.ContainsKey(column))
                {
                    columnIndexes[column] = i;
                }
            }

            foreach (var requiredColumn in requiredColumns)
            {
                if (!columnIndexes.ContainsKey(requiredColumn))
                {
                    result.Errors.Add(new PamaProductionImportErrorDto
                    {
                        RowNumber = 1,
                        ErrorType = "Header",
                        Error =
                            $"Required column '{requiredColumn}' is missing."
                    });

                    result.FailedRows++;
                }
            }

            if (result.FailedRows > 0)
            {
                result.Success = false;

                await CompleteImportAsync(
                    import,
                    result,
                    cancellationToken);

                return result;
            }

            var existingKeys = await _context.PamaProductions
                .AsNoTracking()
                .Where(x => x.SourceId == sourceId)
                .Select(x => new
                {
                    x.ManufacturerName,
                    x.VehicleType,
                    x.Year,
                    x.Month
                })
                .ToListAsync(cancellationToken);

            var duplicateKeys = existingKeys
                .Select(x => BuildDuplicateKey(
                    x.ManufacturerName,
                    x.VehicleType,
                    x.Year,
                    x.Month))
                .ToHashSet();

            string? line;

            while ((line = await reader.ReadLineAsync(
                       cancellationToken)) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                result.TotalRows++;

                var rowNumber = result.TotalRows + 1;

                try
                {
                    var values = ParseCsvLine(line);

                    var manufacturerName =
                        GetValue(values, columnIndexes, "ManufacturerName");

                    var vehicleType =
                        GetValue(values, columnIndexes, "VehicleType");

                    var yearText =
                        GetValue(values, columnIndexes, "Year");

                    var monthText =
                        GetValue(values, columnIndexes, "Month");

                    var productionText =
                        GetValue(
                            values,
                            columnIndexes,
                            "ProductionUnits");

                    var salesText =
                        GetValue(
                            values,
                            columnIndexes,
                            "SalesUnits");

                    var observationDateText =
                        GetValue(
                            values,
                            columnIndexes,
                            "ObservationDate");

                    if (string.IsNullOrWhiteSpace(manufacturerName))
                    {
                        throw new InvalidOperationException(
                            "ManufacturerName is required.");
                    }

                    if (string.IsNullOrWhiteSpace(vehicleType))
                    {
                        throw new InvalidOperationException(
                            "VehicleType is required.");
                    }

                    if (!int.TryParse(
                            yearText,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out var year))
                    {
                        throw new InvalidOperationException(
                            "Year must be a valid number.");
                    }

                    if (year < 1900 || year > 2100)
                    {
                        throw new InvalidOperationException(
                            "Year is outside the supported range.");
                    }

                    if (!int.TryParse(
                            monthText,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out var month) ||
                        month < 1 ||
                        month > 12)
                    {
                        throw new InvalidOperationException(
                            "Month must be between 1 and 12.");
                    }

                    if (!int.TryParse(
                            productionText,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out var productionUnits) ||
                        productionUnits < 0)
                    {
                        throw new InvalidOperationException(
                            "ProductionUnits must be a non-negative number.");
                    }

                    if (!int.TryParse(
                            salesText,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out var salesUnits) ||
                        salesUnits < 0)
                    {
                        throw new InvalidOperationException(
                            "SalesUnits must be a non-negative number.");
                    }

                    if (!DateTime.TryParse(
                            observationDateText,
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out var observationDate))
                    {
                        throw new InvalidOperationException(
                            "ObservationDate must be a valid date.");
                    }

                    var duplicateKey = BuildDuplicateKey(
                        manufacturerName,
                        vehicleType,
                        year,
                        month);

                    if (duplicateKeys.Contains(duplicateKey))
                    {
                        result.DuplicateRows++;

                        result.Errors.Add(
                            new PamaProductionImportErrorDto
                            {
                                RowNumber = rowNumber,
                                ErrorType = "Duplicate",
                                Error =
                                    "A PAMA production record with the same " +
                                    "manufacturer, vehicle type, year and month " +
                                    "already exists."
                            });

                        continue;
                    }

                    var make = await FindMakeAsync(
                        manufacturerName,
                        cancellationToken);

                    var record = new PamaProduction
                    {
                        SourceId = sourceId,
                        ImportId = import.ImportId,
                        MakeId = make?.MakeId,
                        ManufacturerName = manufacturerName.Trim(),
                        VehicleType = vehicleType.Trim(),
                        Year = year,
                        Month = month,
                        ProductionUnits = productionUnits,
                        SalesUnits = salesUnits,
                        ObservationDate = observationDate,
                        CreatedDate = DateTime.UtcNow
                    };

                    _context.PamaProductions.Add(record);

                    duplicateKeys.Add(duplicateKey);

                    result.SuccessfulRows++;
                    result.ImportedRecords++;
                }
                catch (Exception ex)
                {
                    result.FailedRows++;

                    result.Errors.Add(
                        new PamaProductionImportErrorDto
                        {
                            RowNumber = rowNumber,
                            ErrorType = "Validation",
                            Error = ex.Message
                        });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            result.Success = result.FailedRows == 0;

            await CompleteImportAsync(
                import,
                result,
                cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;

            result.Errors.Add(
                new PamaProductionImportErrorDto
                {
                    RowNumber = 0,
                    ErrorType = "Import",
                    Error = ex.Message
                });

            await CompleteImportAsync(
                import,
                result,
                cancellationToken);

            return result;
        }
    }

    private async Task<Make?> FindMakeAsync(
        string manufacturerName,
        CancellationToken cancellationToken)
    {
        var normalizedName = manufacturerName.Trim();

        return await _context.Makes
            .FirstOrDefaultAsync(
                x => x.IsActive &&
                     x.Name == normalizedName,
                cancellationToken);
    }

    private static string BuildDuplicateKey(
        string manufacturerName,
        string vehicleType,
        int year,
        int month)
    {
        return string.Join(
            "|",
            manufacturerName.Trim().ToUpperInvariant(),
            vehicleType.Trim().ToUpperInvariant(),
            year,
            month);
    }

    private static string GetValue(
        IReadOnlyList<string> values,
        IReadOnlyDictionary<string, int> indexes,
        string columnName)
    {
        if (!indexes.TryGetValue(
                columnName,
                out var index))
        {
            return string.Empty;
        }

        if (index < 0 || index >= values.Count)
        {
            return string.Empty;
        }

        return values[index].Trim();
    }

    private static List<string> ParseCsvLine(
        string line)
    {
        var values = new List<string>();
        var current = new System.Text.StringBuilder();

        var insideQuotes = false;

        for (var i = 0; i < line.Length; i++)
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
                }
                else
                {
                    insideQuotes = !insideQuotes;
                }

                continue;
            }

            if (character == ',' && !insideQuotes)
            {
                values.Add(current.ToString());
                current.Clear();
                continue;
            }

            current.Append(character);
        }

        values.Add(current.ToString());

        return values;
    }

    private async Task CompleteImportAsync(
        DataImport import,
        PamaProductionImportResultDto result,
        CancellationToken cancellationToken)
    {
        import.RecordsProcessed = result.TotalRows;
        import.RecordsInserted = result.ImportedRecords;
        import.RecordsRejected = result.FailedRows;
        import.RecordsDuplicated = result.DuplicateRows;

        import.ImportStatus = result.FailedRows == 0
            ? "Completed"
            : "Completed With Errors";

        import.ErrorLog = result.Errors.Count == 0
            ? null
            : string.Join(
                Environment.NewLine,
                result.Errors.Select(x =>
                    $"Row {x.RowNumber}: " +
                    $"[{x.ErrorType}] {x.Error}"));

        import.CompletedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(
            cancellationToken);

        result.ImportId = import.ImportId;
    }
}