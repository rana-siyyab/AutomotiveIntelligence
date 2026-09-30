namespace AutomotiveIntelligence.Application.DTOs;

public class MarketPriceByYearDto
{
    public int Year { get; set; }

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}