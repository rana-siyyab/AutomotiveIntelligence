namespace AutomotiveIntelligence.Web.Models;

public class DataQualityViewModel
{
    public int TotalImports { get; set; }

    public int TotalRecordsProcessed { get; set; }

    public int TotalRecordsInserted { get; set; }

    public int TotalRecordsRejected { get; set; }

    public int TotalRecordsDuplicated { get; set; }

    public decimal RejectionRate { get; set; }

    public decimal DuplicateRate { get; set; }

    public int SuccessfulImports { get; set; }

    public int ImportsWithErrors { get; set; }

    public DateTime? LastImportDate { get; set; }
}