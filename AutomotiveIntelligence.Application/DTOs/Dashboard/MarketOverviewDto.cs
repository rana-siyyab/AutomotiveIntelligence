namespace AutomotiveIntelligence.Application.DTOs.Dashboard;

public class MarketOverviewDto
{
    public int TotalMarketRecords { get; set; }
    public int TotalValuations { get; set; }
    public decimal AverageEstimatedPrice { get; set; }
    public decimal LatestAverageMarketPrice { get; set; }
    public DateTime? LastMarketObservationDate { get; set; }
}