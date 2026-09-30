namespace AutomotiveIntelligence.Web.Models;

public class PamaYearlySummaryViewModel
{
    public int Year { get; set; }

    public int ProductionUnits { get; set; }

    public int SalesUnits { get; set; }

    public decimal ProductionVsSalesRatio { get; set; }
}