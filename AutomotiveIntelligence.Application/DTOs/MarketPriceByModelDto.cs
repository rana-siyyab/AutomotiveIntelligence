namespace AutomotiveIntelligence.Application.DTOs;

public class MarketPriceByModelDto
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public int MakeId { get; set; }

    public string MakeName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}