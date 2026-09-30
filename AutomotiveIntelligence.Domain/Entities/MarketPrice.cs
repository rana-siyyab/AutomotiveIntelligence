namespace AutomotiveIntelligence.Domain.Entities;

public class MarketPrice
{
    public long MarketPriceId { get; set; }

    public int VariantId { get; set; }

    public int Year { get; set; }

    public int CityId { get; set; }

    public int? ConditionId { get; set; }

    public int? Mileage { get; set; }

    public decimal ObservedPrice { get; set; }

    public int? SourceId { get; set; }

    public DateTime ObservationDate { get; set; }

    public DateTime CreatedDate { get; set; }
    // Navigation property
    public Variant Variant { get; set; } = null!;
    public long? SourceRecordId { get; set; }
}