namespace AutomotiveIntelligence.Domain.Entities;

public class Variant
{
    public int VariantId { get; set; }

    public int ModelId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? EngineCapacity { get; set; }

    public string? Transmission { get; set; }

    public string? FuelType { get; set; }

    public string? DriveType { get; set; }

    public int? SeatingCapacity { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
    // Navigation property
    public Model Model { get; set; } = null!;
}