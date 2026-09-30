namespace AutomotiveIntelligence.Application.DTOs;

public class VariantDto
{
    public int VariantId { get; set; }

    public int ModelId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int? EngineCapacity { get; set; }

    public string? Transmission { get; set; }

    public string? FuelType { get; set; }

    public string? DriveType { get; set; }

    public int? SeatingCapacity { get; set; }
}