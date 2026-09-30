namespace AutomotiveIntelligence.Domain.Entities;

public class PriceAlert
{
    public long PriceAlertId { get; set; }

    public int UserId { get; set; }

    public int VariantId { get; set; }

    public int? CityId { get; set; }

    public decimal TargetPrice { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? LastTriggeredDate { get; set; }
}