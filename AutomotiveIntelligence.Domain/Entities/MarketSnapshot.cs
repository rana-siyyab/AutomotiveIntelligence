namespace AutomotiveIntelligence.Domain.Entities;

public class MarketSnapshot
{
    public long SnapshotId { get; set; }

    public int VariantId { get; set; }

    public int CityId { get; set; }

    public DateTime SnapshotDate { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MedianPrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }

    public int ListingCount { get; set; }

    public decimal? DemandScore { get; set; }

    public DateTime CreatedDate { get; set; }
}