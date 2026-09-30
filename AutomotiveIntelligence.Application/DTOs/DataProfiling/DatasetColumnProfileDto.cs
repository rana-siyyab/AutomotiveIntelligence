namespace AutomotiveIntelligence.Application.DTOs.DataProfiling;

public class DatasetColumnProfileDto
{
    public string ColumnName { get; set; } = string.Empty;

    public string DetectedType { get; set; } = string.Empty;

    public long NullCount { get; set; }

    public decimal NullPercentage { get; set; }

    public long UniqueValueCount { get; set; }

    public List<string> SampleValues { get; set; } = [];
}