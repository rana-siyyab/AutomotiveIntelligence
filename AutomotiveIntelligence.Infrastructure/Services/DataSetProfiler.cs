using System.Globalization;
using System.Text;
using AutomotiveIntelligence.Application.DTOs.DataProfiling;
using AutomotiveIntelligence.Application.Services;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class DataSetProfiler : IDataSetProfiler
{
    private const int MaxSampleValues = 5;
    private const int MaxSampleRows = 5;

    public async Task<DatasetProfileDto> ProfileCsvAsync(
        Stream csvStream,
        string fileName,
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

        if (csvStream.CanSeek)
        {
            csvStream.Position = 0;
        }

        using var reader = new StreamReader(
            csvStream,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: true,
            leaveOpen: true);

        var headerLine = await reader.ReadLineAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(headerLine))
        {
            throw new InvalidOperationException(
                "The CSV file does not contain a header row.");
        }

        var headers = ParseCsvLine(headerLine);

        if (headers.Count == 0)
        {
            throw new InvalidOperationException(
                "The CSV file does not contain any columns.");
        }

        var columnStats = headers
            .Select(header => new ColumnStats(header))
            .ToList();

        var sampleRows = new List<DatasetSampleRowDto>();

        long totalRows = 0;

        string? line;

        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            cancellationToken.ThrowIfCancellationRequested();

            var values = ParseCsvLine(line);

            totalRows++;

            var rowDictionary =
                new Dictionary<string, string?>(
                    StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < headers.Count; i++)
            {
                var value =
                    i < values.Count
                        ? NormalizeValue(values[i])
                        : null;

                columnStats[i].AddValue(value);

                rowDictionary[headers[i]] = value;
            }

            if (sampleRows.Count < MaxSampleRows)
            {
                sampleRows.Add(new DatasetSampleRowDto
                {
                    Values = rowDictionary
                });
            }
        }

        var profile = new DatasetProfileDto
        {
            FileName = fileName,
            TotalRows = totalRows,
            TotalColumns = headers.Count,
            SampleRows = sampleRows
        };

        foreach (var column in columnStats)
        {
            profile.Columns.Add(
                column.BuildProfile(totalRows));
        }

        return profile;
    }

    private static string? NormalizeValue(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value.Trim();
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();

        var current = new StringBuilder();

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
                    continue;
                }

                insideQuotes = !insideQuotes;

                continue;
            }

            if (character == ',' && !insideQuotes)
            {
                result.Add(current.ToString());
                current.Clear();

                continue;
            }

            current.Append(character);
        }

        result.Add(current.ToString());

        return result;
    }

    private sealed class ColumnStats
    {
        private readonly HashSet<string> _uniqueValues =
            new(StringComparer.OrdinalIgnoreCase);

        private readonly List<string> _sampleValues = [];

        private long _nullCount;

        private long _valueCount;

        private bool _allIntegers = true;

        private bool _allDecimals = true;

        private bool _allDates = true;

        private bool _allBooleans = true;

        public ColumnStats(string columnName)
        {
            ColumnName = columnName;
        }

        public string ColumnName { get; }

        public void AddValue(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _nullCount++;

                return;
            }

            _valueCount++;

            _uniqueValues.Add(value);

            if (_sampleValues.Count < MaxSampleValues)
            {
                _sampleValues.Add(value);
            }

            if (!int.TryParse(
                    value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out _))
            {
                _allIntegers = false;
            }

            if (!decimal.TryParse(
                    value,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out _))
            {
                _allDecimals = false;
            }

            if (!DateTime.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _))
            {
                _allDates = false;
            }

            if (!bool.TryParse(value, out _))
            {
                _allBooleans = false;
            }
        }

        public DatasetColumnProfileDto BuildProfile(
            long totalRows)
        {
            var detectedType = DetectType();

            var nullPercentage =
                totalRows == 0
                    ? 0
                    : Math.Round(
                        (_nullCount / (decimal)totalRows) * 100,
                        2);

            return new DatasetColumnProfileDto
            {
                ColumnName = ColumnName,
                DetectedType = detectedType,
                NullCount = _nullCount,
                NullPercentage = nullPercentage,
                UniqueValueCount = _uniqueValues.Count,
                SampleValues = [.. _sampleValues]
            };
        }

        private string DetectType()
        {
            if (_valueCount == 0)
            {
                return "Empty";
            }

            if (_allBooleans)
            {
                return "Boolean";
            }

            if (_allIntegers)
            {
                return "Integer";
            }

            if (_allDecimals)
            {
                return "Decimal";
            }

            if (_allDates)
            {
                return "DateTime";
            }

            return "String";
        }
    }
}