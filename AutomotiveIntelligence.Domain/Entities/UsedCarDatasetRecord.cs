namespace AutomotiveIntelligence.Domain.Entities;

public class UsedCarDatasetRecord
{
    public long UsedCarDatasetRecordId { get; set; }

    public int SourceId { get; set; }

    public long? ImportId { get; set; }

    public string? ExternalRecordId { get; set; }

    public string? RawVehicleName { get; set; }

    public string? MakeName { get; set; }

    public string? ModelName { get; set; }

    public string? VariantName { get; set; }

    public int? ManufacturingYear { get; set; }

    public string? RawLocationName { get; set; }

    public string? ProvinceName { get; set; }

    public string? CityName { get; set; }

    public int? Mileage { get; set; }

    public int? EngineCapacity { get; set; }

    public string? Transmission { get; set; }

    public string? FuelType { get; set; }

    public decimal? AskingPrice { get; set; }

    public string? Color { get; set; }

    public string? AssemblyType { get; set; }

    public string? BodyType { get; set; }

    public string? Features { get; set; }

    public string? SellerName { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? ListingDate { get; set; }

    public string? SourceUrl { get; set; }

    public string? RawDataJson { get; set; }

    public DateTime ImportedDate { get; set; }

    public DataSource Source { get; set; } = null!;

    public DataImport? Import { get; set; }
}