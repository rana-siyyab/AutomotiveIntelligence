namespace AutomotiveIntelligence.Domain.Entities;

public class DataSource
{
    public int SourceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? SourceType { get; set; }

    public string? BaseUrl { get; set; }

    public bool IsActive { get; set; } = true;

    // Commercial data compliance
    public string? License { get; set; }

    public bool CommercialUseAllowed { get; set; }

    public bool CommercialTrainingAllowed { get; set; }

    public bool AttributionRequired { get; set; }

    public string? TermsUrl { get; set; }

    public DateTime? AcquiredDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}