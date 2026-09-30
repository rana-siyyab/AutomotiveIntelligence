using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class MarketDataImportService : IMarketDataImportService
{
    private readonly AutomotiveIntelligenceDbContext _context;
    private sealed record MarketObservationKey(
    int SourceId,
    int VariantId,
    int Year,
    int CityId,
    int? Mileage,
    decimal ObservedPrice,
    DateTime ObservationDate);
    public MarketDataImportService(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<MarketDataImportResultDto> ImportAsync(
    Stream csvStream,
    string fileName,
    int sourceId)
    {
        var result = new MarketDataImportResultDto();

        var source =
            await _context.DataSources
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.SourceId == sourceId &&
                    x.IsActive);

        if (source == null)
        {
            result.Success = false;

            result.Errors.Add(
                new MarketDataImportErrorDto
                {
                    RowNumber = 0,
                    Error = "The selected data source does not exist or is inactive."
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
            ImportStatus = "Processing",
            StartedDate = DateTime.UtcNow,
            RecordsDuplicated = 0
        };

        _context.DataImports.Add(import);

        await _context.SaveChangesAsync();

        try
        {
            /*
             * Load existing market observations for this source once.
             *
             * This prevents a database query for every CSV row.
             */
            var existingMarketKeys =
                (await _context.MarketPrices
                    .AsNoTracking()
                    .Where(x => x.SourceId == sourceId)
                    .Select(x => new
                    {
                        x.VariantId,
                        x.Year,
                        x.CityId,
                        x.Mileage,
                        x.ObservedPrice,
                        x.ObservationDate
                    })
                    .ToListAsync())
                .Select(x =>
                    new
                    {
                        x.VariantId,
                        x.Year,
                        x.CityId,
                        x.Mileage,
                        x.ObservedPrice,
                        x.ObservationDate
                    })
                .ToHashSet();

            using var reader =
    new StreamReader(csvStream);

            var headerLine =
                await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(headerLine))
            {
                import.ImportStatus = "Failed";
                import.CompletedDate = DateTime.UtcNow;
                import.ErrorLog = "CSV file is empty.";

                await _context.SaveChangesAsync();

                result.Success = false;

                result.ImportId = import.ImportId;

                result.Errors.Add(
                    new MarketDataImportErrorDto
                    {
                        RowNumber = 1,
                        Error = "CSV file is empty."
                    });

                return result;
            }

            var headers =
                ParseCsvLine(headerLine);

            var headerLookup =
                headers
                    .Select((header, index) =>
                        new
                        {
                            Name = header.Trim(),
                            Index = index
                        })
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(x.Name))
                    .ToDictionary(
                        x => x.Name,
                        x => x.Index,
                        StringComparer.OrdinalIgnoreCase);

            var requiredColumns = new[]
            {
            "Make",
            "Model",
            "Variant",
            "Year",
            "City",
            "ObservedPrice",
            "ObservationDate"
        };

            var missingColumns =
                requiredColumns
                    .Where(x => !headerLookup.ContainsKey(x))
                    .ToList();

            if (missingColumns.Count > 0)
            {
                var message =
                    "Missing required columns: " +
                    string.Join(", ", missingColumns);

                import.ImportStatus = "Failed";
                import.CompletedDate = DateTime.UtcNow;
                import.ErrorLog = message;

                await _context.SaveChangesAsync();

                result.Success = false;
                result.ImportId = import.ImportId;

                result.Errors.Add(
                    new MarketDataImportErrorDto
                    {
                        RowNumber = 1,
                        Error = message
                    });

                return result;
            }

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

                import.RecordsProcessed =
                    result.TotalRows;

                try
                {
                    var values =
                        ParseCsvLine(line);

                    string GetValue(string columnName)
                    {
                        if (!headerLookup.TryGetValue(
                                columnName,
                                out var index))
                        {
                            return string.Empty;
                        }

                        if (index >= values.Count)
                        {
                            return string.Empty;
                        }

                        return values[index].Trim();
                    }

                    var makeName =
                        GetValue("Make");

                    var modelName =
                        GetValue("Model");

                    var variantName =
                        GetValue("Variant");

                    var yearText =
                        GetValue("Year");

                    var cityName =
                        GetValue("City");

                    var mileageText =
                        GetValue("Mileage");

                    var conditionName =
                        GetValue("Condition");

                    var observedPriceText =
                        GetValue("ObservedPrice");

                    var observationDateText =
                        GetValue("ObservationDate");

                    if (string.IsNullOrWhiteSpace(makeName))
                    {
                        throw new InvalidOperationException(
                            "Make is required.");
                    }

                    if (string.IsNullOrWhiteSpace(modelName))
                    {
                        throw new InvalidOperationException(
                            "Model is required.");
                    }

                    if (string.IsNullOrWhiteSpace(variantName))
                    {
                        throw new InvalidOperationException(
                            "Variant is required.");
                    }

                    if (!int.TryParse(
                            yearText,
                            out var year))
                    {
                        throw new InvalidOperationException(
                            "Year is invalid.");
                    }

                    if (year <= 0)
                    {
                        throw new InvalidOperationException(
                            "Year must be greater than zero.");
                    }

                    if (!decimal.TryParse(
                            observedPriceText,
                            System.Globalization.NumberStyles.Number,
                            System.Globalization.CultureInfo.InvariantCulture,
                            out var observedPrice))
                    {
                        throw new InvalidOperationException(
                            "ObservedPrice is invalid.");
                    }

                    if (observedPrice <= 0)
                    {
                        throw new InvalidOperationException(
                            "ObservedPrice must be greater than zero.");
                    }

                    if (!DateTime.TryParse(
                            observationDateText,
                            System.Globalization.CultureInfo.InvariantCulture,
                            System.Globalization.DateTimeStyles.None,
                            out var observationDate))
                    {
                        throw new InvalidOperationException(
                            "ObservationDate is invalid.");
                    }

                    int? mileage = null;

                    if (!string.IsNullOrWhiteSpace(mileageText))
                    {
                        if (!int.TryParse(
                                mileageText,
                                out var mileageValue))
                        {
                            throw new InvalidOperationException(
                                "Mileage is invalid.");
                        }

                        if (mileageValue < 0)
                        {
                            throw new InvalidOperationException(
                                "Mileage cannot be negative.");
                        }

                        mileage = mileageValue;
                    }

                    /*
                     * Resolve Make
                     */
                    var make =
                        await _context.Makes
                            .FirstOrDefaultAsync(x =>
                                x.IsActive &&
                                x.Name.ToLower() ==
                                makeName.ToLower());

                    if (make == null)
                    {
                        throw new InvalidOperationException(
                            $"Make '{makeName}' was not found.");
                    }

                    /*
                     * Resolve Model
                     */
                    var model =
                        await _context.Models
                            .FirstOrDefaultAsync(x =>
                                x.IsActive &&
                                x.MakeId == make.MakeId &&
                                x.Name.ToLower() ==
                                modelName.ToLower());

                    if (model == null)
                    {
                        throw new InvalidOperationException(
                            $"Model '{modelName}' for make '{makeName}' was not found.");
                    }

                    /*
                     * Resolve Variant
                     */
                    var variant =
                        await _context.Variants
                            .FirstOrDefaultAsync(x =>
                                x.IsActive &&
                                x.ModelId == model.ModelId &&
                                x.Name.ToLower() ==
                                variantName.ToLower());

                    if (variant == null)
                    {
                        throw new InvalidOperationException(
                            $"Variant '{variantName}' was not found.");
                    }

                    /*
                     * Resolve City
                     */
                    var city =
                        await _context.Cities
                            .FirstOrDefaultAsync(x =>
                                x.IsActive &&
                                x.Name.ToLower() ==
                                cityName.ToLower());

                    if (city == null)
                    {
                        throw new InvalidOperationException(
                            $"City '{cityName}' was not found.");
                    }

                    /*
                     * Resolve optional condition.
                     */
                    int? conditionId = null;

                    if (!string.IsNullOrWhiteSpace(conditionName))
                    {
                        var condition =
                            await _context.VehicleConditions
                                .FirstOrDefaultAsync(x =>
                                    x.IsActive &&
                                    x.Name.ToLower() ==
                                    conditionName.ToLower());

                        if (condition == null)
                        {
                            throw new InvalidOperationException(
                                $"Condition '{conditionName}' was not found.");
                        }

                        conditionId =
                            condition.ConditionId;
                    }

                    /*
                     * Build the duplicate identity.
                     *
                     * Source is deliberately part of the identity.
                     * The same observation from two legitimate sources
                     * should not automatically be treated as the same
                     * source record.
                     */
                    var marketKey =
                        new
                        {
                            VariantId = variant.VariantId,
                            Year = year,
                            CityId = city.CityId,
                            Mileage = mileage,
                            ObservedPrice = observedPrice,
                            ObservationDate = observationDate
                        };

                    /*
                     * Check whether this exact observation already
                     * exists in the database or has already appeared
                     * earlier in this same CSV.
                     */
                    if (existingMarketKeys.Contains(marketKey))
                    {
                        result.DuplicateRows++;

                        result.Errors.Add(
     new MarketDataImportErrorDto
     {
         RowNumber = rowNumber,
         ErrorType = "Duplicate",
         Error =
             "Duplicate market observation already exists."
     });

                        continue;
                    }

                    /*
                     * Create Vehicle
                     */
                    var vehicle = new Vehicle
                    {
                        VariantId =
                            variant.VariantId,

                        ManufacturingYear =
                            year,

                        Mileage =
                            mileage,

                        CityId =
                            city.CityId,

                        ConditionId =
                            conditionId,

                        IsActive = true,

                        CreatedDate =
                            DateTime.UtcNow
                    };

                    _context.Vehicles.Add(vehicle);

                    await _context.SaveChangesAsync();

                    /*
                     * Create Listing
                     */
                    var listing = new Listing
                    {
                        VehicleId =
                            vehicle.VehicleId,

                        CityId =
                            city.CityId,

                        AskingPrice =
                            observedPrice,

                        ListingDate =
                            observationDate,

                        SourceId =
                            sourceId,

                        ListingStatus =
                            "Active",

                        CreatedDate =
                            DateTime.UtcNow
                    };

                    _context.Listings.Add(listing);

                    /*
                     * Create Market Price
                     */
                    var marketPrice = new MarketPrice
                    {
                        VariantId =
                            variant.VariantId,

                        Year =
                            year,

                        CityId =
                            city.CityId,

                        ConditionId =
                            conditionId,

                        Mileage =
                            mileage,

                        ObservedPrice =
                            observedPrice,

                        SourceId =
                            sourceId,

                        ObservationDate =
                            observationDate,

                        CreatedDate =
                            DateTime.UtcNow
                    };

                    _context.MarketPrices.Add(marketPrice);

                    await _context.SaveChangesAsync();

                    /*
                     * Add the newly imported observation to the
                     * HashSet so duplicate rows later in the SAME
                     * CSV are detected as well.
                     */
                    existingMarketKeys.Add(marketKey);

                    result.SuccessfulRows++;
                    result.ImportedVehicles++;
                    result.ImportedListings++;
                    result.ImportedMarketPrices++;
                }
                catch (Exception ex)
                {
                    result.FailedRows++;

                    result.Errors.Add(
     new MarketDataImportErrorDto
     {
         RowNumber = rowNumber,
         ErrorType = "Validation",
         Error = ex.Message
     });
                }
            }

            import.RecordsProcessed =
                result.TotalRows;

            import.RecordsInserted =
                result.ImportedMarketPrices;

            import.RecordsRejected =
     result.FailedRows;

            import.RecordsDuplicated =
                result.DuplicateRows;

            import.ImportStatus =
                result.FailedRows == 0
                    ? "Completed"
                    : "Completed With Errors";

            import.ErrorLog =
                result.Errors.Count > 0
                    ? string.Join(
                        Environment.NewLine,
                        result.Errors.Select(x =>
                            $"Row {x.RowNumber}: {x.Error}"))
                    : null;

            import.CompletedDate =
                DateTime.UtcNow;

            await _context.SaveChangesAsync();

            result.ImportId =
                import.ImportId;

            /*
             * Duplicate rows are not considered import failures.
             * They are successfully identified and skipped.
             */
            result.Success =
                result.FailedRows == 0;

            return result;
        }
        catch (Exception ex)
        {
            import.ImportStatus = "Failed";

            import.CompletedDate =
                DateTime.UtcNow;

            import.ErrorLog =
                ex.ToString();

            await _context.SaveChangesAsync();

            result.Success = false;

            result.ImportId =
                import.ImportId;

            result.Errors.Add(
                new MarketDataImportErrorDto
                {
                    RowNumber = 0,
                    Error = ex.Message
                });

            return result;
        }

        static List<string> ParseCsvLine(string line)
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

                if (character == ',' &&
                    !insideQuotes)
                {
                    values.Add(
                        current.ToString());

                    current.Clear();

                    continue;
                }

                current.Append(character);
            }

            values.Add(
                current.ToString());

            return values;
        }
    }


    private async Task ImportRowAsync(
        MarketDataImportRowDto row,
        int sourceId)
    {
        var make = await _context.Makes
            .FirstOrDefaultAsync(x =>
                x.IsActive &&
                x.Name.ToLower() ==
                row.Make.Trim().ToLower());

        if (make == null)
        {
            throw new InvalidOperationException(
                $"Make '{row.Make}' was not found.");
        }


        var model = await _context.Models
            .FirstOrDefaultAsync(x =>
                x.IsActive &&
                x.MakeId == make.MakeId &&
                x.Name.ToLower() ==
                row.Model.Trim().ToLower());

        if (model == null)
        {
            throw new InvalidOperationException(
                $"Model '{row.Model}' for make '{row.Make}' was not found.");
        }


        var variant = await _context.Variants
            .FirstOrDefaultAsync(x =>
                x.IsActive &&
                x.ModelId == model.ModelId &&
                x.Name.ToLower() ==
                row.Variant.Trim().ToLower());

        if (variant == null)
        {
            throw new InvalidOperationException(
                $"Variant '{row.Variant}' for model '{row.Model}' was not found.");
        }


        var city = await _context.Cities
            .FirstOrDefaultAsync(x =>
                x.Name.ToLower() ==
                row.City.Trim().ToLower());

        if (city == null)
        {
            throw new InvalidOperationException(
                $"City '{row.City}' was not found.");
        }


        int? conditionId = null;

        if (!string.IsNullOrWhiteSpace(row.Condition))
        {
            var condition =
                await _context.VehicleConditions
                    .FirstOrDefaultAsync(x =>
                        x.IsActive &&
                        x.Name.ToLower() ==
                        row.Condition.Trim().ToLower());

            if (condition == null)
            {
                throw new InvalidOperationException(
                    $"Condition '{row.Condition}' was not found.");
            }

            conditionId =
                condition.ConditionId;
        }


        if (!row.Year.HasValue)
        {
            throw new InvalidOperationException(
                "Year is required for market data import.");
        }


        if (!row.ObservedPrice.HasValue ||
            row.ObservedPrice.Value <= 0)
        {
            throw new InvalidOperationException(
                "ObservedPrice must be greater than zero.");
        }


        if (!row.ObservationDate.HasValue)
        {
            throw new InvalidOperationException(
                "ObservationDate is required.");
        }


        var vehicle = new Vehicle
        {
            VariantId = variant.VariantId,

            ManufacturingYear =
                row.Year.Value,

            Mileage =
                row.Mileage,

            CityId =
                city.CityId,

            ConditionId =
                conditionId,

            IsActive = true,

            CreatedDate =
                DateTime.UtcNow
        };

        _context.Vehicles.Add(vehicle);

        await _context.SaveChangesAsync();


        var listing = new Listing
        {
            VehicleId =
                vehicle.VehicleId,

            CityId =
                city.CityId,

            AskingPrice =
                row.ObservedPrice.Value,

            SellerType =
                "Imported",

            ListingDate =
                row.ObservationDate.Value,

            SourceId =
                sourceId,

            SourceReference =
                $"{row.Make}-{row.Model}-{row.Variant}-{row.Year}",

            ListingStatus =
                "Imported",

            CreatedDate =
                DateTime.UtcNow
        };

        _context.Listings.Add(listing);

        var marketPrice = new MarketPrice
        {
            VariantId =
                variant.VariantId,

            Year =
                row.Year.Value,

            CityId =
                city.CityId,

            ConditionId =
                conditionId,

            Mileage =
                row.Mileage,

            ObservedPrice =
                row.ObservedPrice.Value,

            SourceId =
                sourceId,

            ObservationDate =
                row.ObservationDate.Value,

            CreatedDate =
                DateTime.UtcNow
        };

        _context.MarketPrices.Add(marketPrice);

        await _context.SaveChangesAsync();
    }


    private static MarketDataImportRowDto ParseRow(
        IReadOnlyList<string> values,
        IReadOnlyDictionary<string, int> headerMap,
        int rowNumber)
    {
        return new MarketDataImportRowDto
        {
            RowNumber = rowNumber,

            Make =
                GetValue(values, headerMap, "Make"),

            Model =
                GetValue(values, headerMap, "Model"),

            Variant =
                GetValue(values, headerMap, "Variant"),

            Year =
                ParseNullableInt(
                    GetValue(values, headerMap, "Year")),

            City =
                GetValue(values, headerMap, "City"),

            Mileage =
                ParseNullableInt(
                    GetValue(values, headerMap, "Mileage")),

            Condition =
                GetOptionalValue(
                    values,
                    headerMap,
                    "Condition"),

            ObservedPrice =
                ParseNullableDecimal(
                    GetValue(
                        values,
                        headerMap,
                        "ObservedPrice")),

            ObservationDate =
                ParseNullableDate(
                    GetValue(
                        values,
                        headerMap,
                        "ObservationDate"))
        };
    }


    private static void ValidateRequiredHeaders(
        IReadOnlyDictionary<string, int> headerMap)
    {
        var requiredHeaders = new[]
        {
            "Make",
            "Model",
            "Variant",
            "Year",
            "City",
            "ObservedPrice",
            "ObservationDate"
        };

        var missingHeaders =
            requiredHeaders
                .Where(x => !headerMap.ContainsKey(x))
                .ToList();

        if (missingHeaders.Count > 0)
        {
            throw new InvalidOperationException(
                "Missing required CSV columns: " +
                string.Join(", ", missingHeaders));
        }
    }


    private static string GetValue(
        IReadOnlyList<string> values,
        IReadOnlyDictionary<string, int> headerMap,
        string header)
    {
        if (!headerMap.TryGetValue(
                header,
                out var index))
        {
            return string.Empty;
        }

        if (index >= values.Count)
        {
            return string.Empty;
        }

        return values[index].Trim();
    }


    private static string? GetOptionalValue(
        IReadOnlyList<string> values,
        IReadOnlyDictionary<string, int> headerMap,
        string header)
    {
        var value =
            GetValue(
                values,
                headerMap,
                header);

        return string.IsNullOrWhiteSpace(value)
            ? null
            : value;
    }


    private static int? ParseNullableInt(
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (int.TryParse(
                value.Trim(),
                out var result))
        {
            return result;
        }

        throw new InvalidOperationException(
            $"'{value}' is not a valid integer.");
    }


    private static decimal? ParseNullableDecimal(
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (decimal.TryParse(
                value.Trim(),
                out var result))
        {
            return result;
        }

        throw new InvalidOperationException(
            $"'{value}' is not a valid decimal number.");
    }


    private static DateTime? ParseNullableDate(
        string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (DateTime.TryParse(
                value.Trim(),
                out var result))
        {
            return result;
        }

        throw new InvalidOperationException(
            $"'{value}' is not a valid date.");
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
                    insideQuotes =
                        !insideQuotes;
                }

                continue;
            }

            if (character == ',' &&
                !insideQuotes)
            {
                values.Add(
                    current.ToString());

                current.Clear();

                continue;
            }

            current.Append(character);
        }

        values.Add(
            current.ToString());

        return values;
    }
}