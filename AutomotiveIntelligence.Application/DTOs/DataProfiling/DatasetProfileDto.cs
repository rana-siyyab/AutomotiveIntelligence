namespace AutomotiveIntelligence.Application.DTOs.DataProfiling;

public class DatasetProfileDto
{
    public string FileName { get; set; } = string.Empty;

    public long TotalRows { get; set; }

    public int TotalColumns { get; set; }

    public List<DatasetColumnProfileDto> Columns { get; set; } = [];

    public List<DatasetSampleRowDto> SampleRows { get; set; } = [];
}