namespace AutomotiveIntelligence.Application.DTOs;

public class ExternalDataFetchResultDto
{
    public bool Success { get; set; }

    public int SourceId { get; set; }

    public string SourceName { get; set; } = string.Empty;

    public string? FileName { get; set; }

    public string? ContentType { get; set; }

    public Stream? Data { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime RetrievedDate { get; set; }
}