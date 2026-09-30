namespace AutomotiveIntelligence.Web.Models;

public class ValuationResultViewModel
{
    public long ValuationId { get; set; }

    public decimal EstimatedPrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }

    public decimal ConfidenceScore { get; set; }

    public string DealAssessment { get; set; } = string.Empty;

    public decimal? RecommendedBuyingPrice { get; set; }

    public decimal? RecommendedNegotiationPrice { get; set; }

    public string ValuationMethod { get; set; } = string.Empty;

    public List<ValuationFactorViewModel> Factors { get; set; } = [];
}

public class ValuationFactorViewModel
{
    public string FactorType { get; set; } = string.Empty;

    public string FactorName { get; set; } = string.Empty;

    public decimal AdjustmentValue { get; set; }

    public decimal? AdjustmentPercentage { get; set; }

    public string? Description { get; set; }
}