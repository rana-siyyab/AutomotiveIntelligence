namespace AutomotiveIntelligence.Application.DTOs;

public class ValuationFactorDto
{
    public string FactorType { get; set; } = string.Empty;

    public string FactorName { get; set; } = string.Empty;

    public decimal AdjustmentValue { get; set; }

    public decimal? AdjustmentPercentage { get; set; }

    public string? Description { get; set; }
}