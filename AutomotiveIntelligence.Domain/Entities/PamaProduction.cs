namespace AutomotiveIntelligence.Domain.Entities;

public class PamaProduction
{
    public long PamaProductionId { get; set; }

    public int SourceId { get; set; }

    public long? ImportId { get; set; }

    public int? MakeId { get; set; }

    public string ManufacturerName { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Month { get; set; }

    public int ProductionUnits { get; set; }

    public int SalesUnits { get; set; }

    public DateTime ObservationDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public Make? Make { get; set; }
}