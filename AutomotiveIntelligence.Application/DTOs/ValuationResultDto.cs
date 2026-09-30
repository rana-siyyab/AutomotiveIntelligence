namespace AutomotiveIntelligence.Application.DTOs;

public class ValuationResultDto
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

    public List<ValuationFactorDto> Factors { get; set; } = [];
}