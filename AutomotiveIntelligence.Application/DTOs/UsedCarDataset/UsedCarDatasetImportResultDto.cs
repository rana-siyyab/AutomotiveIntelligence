namespace AutomotiveIntelligence.Application.DTOs.UsedCarDataset;

public class UsedCarDatasetImportResultDto
{
    public bool Success { get; set; }

    public long ImportId { get; set; }

    public int TotalRows { get; set; }

    public int ImportedRows { get; set; }

    public int DuplicateRows { get; set; }

    public int RejectedRows { get; set; }

    public List<UsedCarDatasetImportErrorDto> Errors { get; set; } = [];
}