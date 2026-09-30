namespace AutomotiveIntelligence.Application.DTOs;

public class UsedCarDatasetNormalizationResultDto
{
    public int SourceId { get; set; }

    public int ProcessedRecords { get; set; }

    public int NormalizedRecords { get; set; }

    public int PendingRecords { get; set; }

    public int InvalidYearRecords { get; set; }

    public int MissingPriceRecords { get; set; }

    public int HighPriceRecords { get; set; }

    public int UnknownLocationRecords { get; set; }

    public int ProvinceRecords { get; set; }

    public int CityRecords { get; set; }

    public int UnregisteredRecords { get; set; }

    public int BasicValuationReadyRecords { get; set; }

    public int DetailedValuationReadyRecords { get; set; }

    public string Status { get; set; } = string.Empty;
}