namespace AutomotiveIntelligence.Application.DTOs;

public class DataSourceDto
{
    public int SourceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SourceType { get; set; } = string.Empty;

    public string? BaseUrl { get; set; }

    public bool IsActive { get; set; }

    public string? License { get; set; }

    public bool CommercialUseAllowed { get; set; }

    public bool CommercialTrainingAllowed { get; set; }

    public bool AttributionRequired { get; set; }

    public string? TermsUrl { get; set; }

    public DateTime? AcquiredDate { get; set; }
}