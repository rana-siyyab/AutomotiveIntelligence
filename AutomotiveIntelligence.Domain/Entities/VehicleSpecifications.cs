namespace AutomotiveIntelligence.Domain.Entities;

public class VehicleSpecifications
{
    public int VehicleSpecificationsId { get; set; }

    public int VariantId { get; set; }

    public int? EngineCapacity { get; set; }

    public int? Horsepower { get; set; }

    public int? Torque { get; set; }

    public string? Transmission { get; set; }

    public string? FuelType { get; set; }

    public string? DriveType { get; set; }

    public int? SeatingCapacity { get; set; }

    public decimal? Length { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }

    public decimal? Wheelbase { get; set; }

    public decimal? GroundClearance { get; set; }

    public decimal? FuelTankCapacity { get; set; }

    public decimal? BootSpace { get; set; }

    public int? AirbagCount { get; set; }

    public bool? HasABS { get; set; }

    public bool? HasSunroof { get; set; }

    public bool? HasTractionControl { get; set; }

    public bool? HasStabilityControl { get; set; }

    public bool? HasInfotainment { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}