namespace AutomotiveIntelligence.Domain.Entities;

public class Valuation
{
    public long ValuationId { get; set; }

    public int? UserId { get; set; }

    public int? VehicleId { get; set; }

    public int VariantId { get; set; }

    public int ManufacturingYear { get; set; }

    public int? Mileage { get; set; }

    public int CityId { get; set; }

    public int? ConditionId { get; set; }

    public decimal? AskingPrice { get; set; }

    public decimal EstimatedPrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
    public string? DealAssessment { get; set; }

    public decimal? RecommendedBuyingPrice { get; set; }

    public decimal? RecommendedNegotiationPrice { get; set; }

    public decimal ConfidenceScore { get; set; }

    public string? ValuationMethod { get; set; }

    public DateTime CreatedDate { get; set; }
}