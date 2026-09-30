namespace AutomotiveIntelligence.Domain.Entities;

public class DataImport
{
    public long ImportId { get; set; }

    public int SourceId { get; set; }

    public string? FileName { get; set; }

    public int RecordsProcessed { get; set; }

    public int RecordsInserted { get; set; }

    public int RecordsRejected { get; set; }

    public int RecordsDuplicated { get; set; }

    public string? ImportStatus { get; set; }

    public string? ErrorLog { get; set; }

    public DateTime StartedDate { get; set; }

    public DateTime? CompletedDate { get; set; }
}