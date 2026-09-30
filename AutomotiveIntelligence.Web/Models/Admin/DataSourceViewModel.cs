namespace AutomotiveIntelligence.Web.Models.Admin;

public class DataSourceViewModel
{
    public int SourceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SourceType { get; set; } = string.Empty;

    public string? BaseUrl { get; set; }

    public bool IsActive { get; set; }
}