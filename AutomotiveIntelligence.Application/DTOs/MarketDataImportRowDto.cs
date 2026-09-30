namespace AutomotiveIntelligence.Application.DTOs;

public class MarketDataImportRowDto
{
    public int RowNumber { get; set; }

    public string Make { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Variant { get; set; } = string.Empty;

    public int? Year { get; set; }

    public string City { get; set; } = string.Empty;

    public int? Mileage { get; set; }

    public string? Condition { get; set; }

    public decimal? ObservedPrice { get; set; }

    public DateTime? ObservationDate { get; set; }
}