namespace AutomotiveIntelligence.Domain.Entities;

public class UsedCarDatasetNormalization
{
    public long NormalizationId { get; set; }

    public long UsedCarDatasetRecordId { get; set; }

    public int SourceId { get; set; }

    public string NormalizationStatus { get; set; } = "Pending";

    public string? NormalizedMakeName { get; set; }

    public string? NormalizedModelName { get; set; }

    public string? NormalizedVariantName { get; set; }

    public string? NormalizedLocationName { get; set; }

    public string? LocationType { get; set; }

    public int? CityId { get; set; }

    public int? ProvinceId { get; set; }

    public string? YearStatus { get; set; }

    public string? PriceStatus { get; set; }

    public bool IsBasicValuationReady { get; set; }

    public bool IsDetailedValuationReady { get; set; }

    public string? NormalizationNotes { get; set; }

    public DateTime NormalizedDate { get; set; }

    public UsedCarDatasetRecord UsedCarDatasetRecord { get; set; } = null!;
}