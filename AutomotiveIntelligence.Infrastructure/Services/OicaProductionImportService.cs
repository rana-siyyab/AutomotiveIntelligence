using System.Globalization;
using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class OicaProductionImportService
    : IOicaProductionImportService
{
    private readonly AutomotiveIntelligenceDbContext _context;
    private readonly IDataSourceRepository _dataSourceRepository;

    public OicaProductionImportService(
        AutomotiveIntelligenceDbContext context,
        IDataSourceRepository dataSourceRepository)
    {
        _context = context;
        _dataSourceRepository = dataSourceRepository;
    }

    public async Task<OicaProductionImportResultDto> ImportAsync(
        Stream dataStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        var result = new OicaProductionImportResultDto();

        var source = await _dataSourceRepository
            .GetByIdAsync(sourceId);

        if (source == null)
        {
            result.Success = false;

            result.Errors.Add(new OicaProductionImportErrorDto
            {
                RowNumber = 0,
                ErrorType = "Source",
                Error = $"Data source with ID {sourceId} was not found or is inactive."
            });

            return result;
        }

        var import = new DataImport
        {
            SourceId = sourceId,
            FileName = fileName,
            ImportStatus = "Processing",
            StartedDate = DateTime.UtcNow
        };

        _context.DataImports.Add(import);

        await _context.SaveChangesAsync(cancellationToken);

        result.ImportId = import.ImportId;

        try
        {
            using var reader = new StreamReader(dataStream);

            var headerLine = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new InvalidOperationException(
                    "The CSV file is empty.");
            }

            var headers = ParseCsvLine(headerLine);

            var requiredHeaders = new[]
            {
                "CountryName",
                "VehicleType",
                "Year",
                "ProductionUnits",
                "ObservationDate"
            };

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!headers.Any(x =>
                    x.Equals(
                        requiredHeader,
                        StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException(
                        $"Required CSV column '{requiredHeader}' is missing.");
                }
            }

            var headerIndexes = headers
                .Select((name, index) => new
                {
                    name = name.Trim(),
                    index
                })
                .ToDictionary(
                    x => x.name,
                    x => x.index,
                    StringComparer.OrdinalIgnoreCase);

            var existingKeys = await _context.OicaProductions
                .AsNoTracking()
                .Where(x => x.SourceId == sourceId)
                .Select(x => new
                {
                    x.CountryName,
                    x.VehicleType,
                    x.Year
                })
                .ToListAsync(cancellationToken);

            var duplicateKeys = existingKeys
                .Select(x => BuildDuplicateKey(
                    x.CountryName,
                    x.VehicleType,
                    x.Year))
                .ToHashSet();

            string? line;
            var rowNumber = 1;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                rowNumber++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                result.TotalRows++;

                try
                {
                    var columns = ParseCsvLine(line);

                    string GetValue(string columnName)
                    {
                        var index = headerIndexes[columnName];

                        return index < columns.Count
                            ? columns[index].Trim()
                            : string.Empty;
                    }

                    var countryName = GetValue("CountryName");
                    var vehicleType = GetValue("VehicleType");
                    var yearText = GetValue("Year");
                    var productionText = GetValue("ProductionUnits");
                    var observationDateText =
                        GetValue("ObservationDate");

                    if (string.IsNullOrWhiteSpace(countryName))
                    {
                        throw new InvalidOperationException(
                            "CountryName is required.");
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
                            "Year must be between 1900 and 2100.");
                    }

                    if (!int.TryParse(
                            productionText,
                            NumberStyles.Integer,
                            CultureInfo.InvariantCulture,
                            out var productionUnits))
                    {
                        throw new InvalidOperationException(
                            "ProductionUnits must be a valid number.");
                    }

                    if (productionUnits < 0)
                    {
                        throw new InvalidOperationException(
                            "ProductionUnits cannot be negative.");
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
                        countryName,
                        vehicleType,
                        year);

                    if (duplicateKeys.Contains(duplicateKey))
                    {
                        result.DuplicateRows++;

                        result.Errors.Add(
                            new OicaProductionImportErrorDto
                            {
                                RowNumber = rowNumber,
                                ErrorType = "Duplicate",
                                Error =
                                    "An OICA production record with the same country, vehicle type and year already exists."
                            });

                        continue;
                    }

                    var country = await _context.Countries
                        .FirstOrDefaultAsync(
                            x => x.Name == countryName &&
                                 x.IsActive,
                            cancellationToken);

                    var production = new OicaProduction
                    {
                        SourceId = sourceId,
                        ImportId = import.ImportId,
                        CountryId = country?.CountryId,
                        CountryName = countryName,
                        VehicleType = vehicleType,
                        Year = year,
                        ProductionUnits = productionUnits,
                        ObservationDate = observationDate,
                        CreatedDate = DateTime.UtcNow
                    };

                    _context.OicaProductions.Add(production);

                    duplicateKeys.Add(duplicateKey);

                    result.SuccessfulRows++;
                    result.ImportedRecords++;
                }
                catch (Exception ex)
                {
                    result.FailedRows++;

                    result.Errors.Add(
                        new OicaProductionImportErrorDto
                        {
                            RowNumber = rowNumber,
                            ErrorType = "Validation",
                            Error = ex.Message
                        });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            import.RecordsProcessed = result.TotalRows;
            import.RecordsInserted = result.ImportedRecords;
            import.RecordsRejected = result.FailedRows;
            import.RecordsDuplicated = result.DuplicateRows;
            import.ImportStatus =
                result.FailedRows == 0
                    ? "Completed"
                    : "CompletedWithErrors";
            import.CompletedDate = DateTime.UtcNow;

            if (result.Errors.Count > 0)
            {
                import.ErrorLog = string.Join(
                    Environment.NewLine,
                    result.Errors.Select(x =>
                        $"Row {x.RowNumber}: {x.ErrorType} - {x.Error}"));
            }

            await _context.SaveChangesAsync(cancellationToken);

            result.Success = result.FailedRows == 0;

            return result;
        }
        catch (Exception ex)
        {
            import.ImportStatus = "Failed";
            import.CompletedDate = DateTime.UtcNow;
            import.ErrorLog = ex.Message;

            await _context.SaveChangesAsync(cancellationToken);

            result.Success = false;

            result.Errors.Add(
                new OicaProductionImportErrorDto
                {
                    RowNumber = 0,
                    ErrorType = "Import",
                    Error = ex.Message
                });

            return result;
        }
    }

    private static string BuildDuplicateKey(
        string countryName,
        string vehicleType,
        int year)
    {
        return string.Join(
            "|",
            countryName.Trim().ToUpperInvariant(),
            vehicleType.Trim().ToUpperInvariant(),
            year);
    }

    private static List<string> ParseCsvLine(string line)
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
}