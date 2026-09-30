namespace AutomotiveIntelligence.Application.DTOs;

public class MarketIntelligenceOverviewDto
{
    public int TotalMarketRecords { get; set; }

    public int TotalMakes { get; set; }

    public int TotalModels { get; set; }

    public int TotalCities { get; set; }

    public decimal AverageMarketPrice { get; set; }

    public decimal MinimumMarketPrice { get; set; }

    public decimal MaximumMarketPrice { get; set; }

    public decimal LatestAverageMarketPrice { get; set; }

    public DateTime? LatestObservationDate { get; set; }
}