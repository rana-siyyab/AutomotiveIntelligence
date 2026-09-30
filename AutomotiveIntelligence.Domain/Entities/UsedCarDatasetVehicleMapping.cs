namespace AutomotiveIntelligence.Domain.Entities;

public class UsedCarDatasetVehicleMapping
{
    public long MappingId { get; set; }

    public long UsedCarDatasetRecordId { get; set; }

    public int SourceId { get; set; }

    public int? MakeId { get; set; }

    public int? ModelId { get; set; }

    public int? VariantId { get; set; }

    public string MappingStatus { get; set; } = "Pending";

    public string? MappingMethod { get; set; }

    public decimal? ConfidenceScore { get; set; }

    public string? MappingNotes { get; set; }

    public DateTime MappedDate { get; set; }

    public UsedCarDatasetRecord UsedCarDatasetRecord { get; set; } = null!;

    public Make? Make { get; set; }

    public Model? Model { get; set; }

    public Variant? Variant { get; set; }
}