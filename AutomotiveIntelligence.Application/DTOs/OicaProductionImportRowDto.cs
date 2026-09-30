namespace AutomotiveIntelligence.Application.DTOs;

public class OicaProductionImportRowDto
{
    public int RowNumber { get; set; }

    public string CountryName { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public int? Year { get; set; }

    public int? ProductionUnits { get; set; }

    public DateTime? ObservationDate { get; set; }
}