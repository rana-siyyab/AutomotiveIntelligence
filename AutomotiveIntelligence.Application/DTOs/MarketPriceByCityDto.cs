namespace AutomotiveIntelligence.Application.DTOs;

public class MarketPriceByCityDto
{
    public int CityId { get; set; }

    public string CityName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}