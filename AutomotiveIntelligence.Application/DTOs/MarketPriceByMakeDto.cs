namespace AutomotiveIntelligence.Application.DTOs;

public class MarketPriceByMakeDto
{
    public int MakeId { get; set; }

    public string MakeName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}