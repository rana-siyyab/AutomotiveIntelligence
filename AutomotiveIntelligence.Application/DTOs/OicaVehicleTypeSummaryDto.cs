namespace AutomotiveIntelligence.Application.DTOs;

public class OicaVehicleTypeSummaryDto
{
    public string VehicleType { get; set; } = string.Empty;

    public int ProductionUnits { get; set; }

    public decimal ProductionSharePercentage { get; set; }
}