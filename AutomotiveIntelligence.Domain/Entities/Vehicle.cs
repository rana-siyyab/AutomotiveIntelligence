namespace AutomotiveIntelligence.Domain.Entities;

public class Vehicle
{
    public int VehicleId { get; set; }

    public int VariantId { get; set; }

    public int ManufacturingYear { get; set; }

    public int? RegistrationYear { get; set; }

    public int? Mileage { get; set; }

    public int? CityId { get; set; }

    public string? Color { get; set; }

    public int? ConditionId { get; set; }

    public int? OwnerCount { get; set; }

    public bool? HasAccidentHistory { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}