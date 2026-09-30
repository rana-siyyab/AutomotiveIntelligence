namespace AutomotiveIntelligence.Web.Models.Admin;

public class DataImportHistoryViewModel
{
    public IReadOnlyList<DataImportViewModel> Imports { get; set; }
        = [];

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}

public class DataImportViewModel
{
    public long ImportId { get; set; }

    public int SourceId { get; set; }

    public string SourceName { get; set; } = string.Empty;

    public string SourceType { get; set; } = string.Empty;

    public string? FileName { get; set; }
    public int RecordsDuplicated { get; set; }
    public int RecordsProcessed { get; set; }

    public int RecordsInserted { get; set; }

    public int RecordsRejected { get; set; }

    public string? ImportStatus { get; set; }

    public DateTime StartedDate { get; set; }

    public DateTime? CompletedDate { get; set; }
}