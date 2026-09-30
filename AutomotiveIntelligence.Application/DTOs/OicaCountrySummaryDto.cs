namespace AutomotiveIntelligence.Application.DTOs;

public class OicaCountrySummaryDto
{
    public string CountryName { get; set; } = string.Empty;

    public int ProductionUnits { get; set; }

    public decimal ProductionSharePercentage { get; set; }
}