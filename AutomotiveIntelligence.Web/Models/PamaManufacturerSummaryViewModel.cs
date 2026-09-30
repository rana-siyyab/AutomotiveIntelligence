namespace AutomotiveIntelligence.Web.Models;

public class PamaManufacturerSummaryViewModel
{
    public int? MakeId { get; set; }

    public string ManufacturerName { get; set; } = string.Empty;

    public int ProductionUnits { get; set; }

    public int SalesUnits { get; set; }

    public decimal ProductionSharePercentage { get; set; }

    public decimal SalesSharePercentage { get; set; }
}