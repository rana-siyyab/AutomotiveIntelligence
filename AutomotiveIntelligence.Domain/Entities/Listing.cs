namespace AutomotiveIntelligence.Domain.Entities;

public class Listing
{
    public long ListingId { get; set; }

    public int VehicleId { get; set; }

    public int CityId { get; set; }

    public decimal AskingPrice { get; set; }

    public string? SellerType { get; set; }

    public DateTime ListingDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? SourceId { get; set; }

    public string? SourceReference { get; set; }

    public string? ListingUrl { get; set; }

    public string? ListingStatus { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}