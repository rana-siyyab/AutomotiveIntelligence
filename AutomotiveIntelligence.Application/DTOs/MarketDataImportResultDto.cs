namespace AutomotiveIntelligence.Application.DTOs;

public class MarketDataImportResultDto
{
    public bool Success { get; set; }

    public long ImportId { get; set; }

    public int TotalRows { get; set; }

    public int SuccessfulRows { get; set; }

    public int FailedRows { get; set; }

    public int DuplicateRows { get; set; }

    public int ImportedVehicles { get; set; }

    public int ImportedListings { get; set; }

    public int ImportedMarketPrices { get; set; }

    public List<MarketDataImportErrorDto> Errors { get; set; } = [];
}