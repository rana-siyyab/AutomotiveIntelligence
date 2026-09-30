namespace AutomotiveIntelligence.Application.DTOs;

public class UsedCarMarketProjectionResultDto
{
    public int SourceId { get; set; }

    public int ProcessedRecords { get; set; }

    public int ProjectedRecords { get; set; }

    public int SkippedRecords { get; set; }

    public int DuplicateRecords { get; set; }

    public int InvalidYearRecords { get; set; }

    public int MissingPriceRecords { get; set; }

    public int UnknownLocationRecords { get; set; }

    public int ProvinceLocationRecords { get; set; }

    public int CityLocationRecords { get; set; }

    public int MarketPriceRecordsCreated { get; set; }

    public int PendingRecords { get; set; }

    public string Status { get; set; } = string.Empty;
}