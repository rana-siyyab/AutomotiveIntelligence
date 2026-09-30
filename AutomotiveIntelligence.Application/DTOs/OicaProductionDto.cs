namespace AutomotiveIntelligence.Application.DTOs;

public class OicaProductionDto
{
    public long OicaProductionId { get; set; }

    public int SourceId { get; set; }

    public long? ImportId { get; set; }

    public int? CountryId { get; set; }

    public string CountryName { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public int Year { get; set; }

    public int ProductionUnits { get; set; }

    public DateTime ObservationDate { get; set; }
}