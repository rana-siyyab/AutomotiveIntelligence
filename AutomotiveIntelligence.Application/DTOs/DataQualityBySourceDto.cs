namespace AutomotiveIntelligence.Application.DTOs;

public class DataQualityBySourceDto
{
    public int SourceId { get; set; }

    public string SourceName { get; set; } = string.Empty;

    public string SourceType { get; set; } = string.Empty;

    public int TotalImports { get; set; }

    public int RecordsProcessed { get; set; }

    public int RecordsInserted { get; set; }

    public int RecordsRejected { get; set; }

    public int RecordsDuplicated { get; set; }

    public decimal RejectionRate { get; set; }

    public decimal DuplicateRate { get; set; }
}