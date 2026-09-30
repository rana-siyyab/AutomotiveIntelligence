namespace AutomotiveIntelligence.Domain.Entities;

public class ValuationFactor
{
    public long ValuationFactorId { get; set; }

    public long ValuationId { get; set; }

    public string FactorType { get; set; } = string.Empty;

    public string FactorName { get; set; } = string.Empty;

    public decimal AdjustmentValue { get; set; }

    public decimal? AdjustmentPercentage { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }
}