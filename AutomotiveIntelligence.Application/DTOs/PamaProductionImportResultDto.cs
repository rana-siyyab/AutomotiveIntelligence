namespace AutomotiveIntelligence.Application.DTOs;

public class PamaProductionImportResultDto
{
    public bool Success { get; set; }

    public long ImportId { get; set; }

    public int TotalRows { get; set; }

    public int SuccessfulRows { get; set; }

    public int FailedRows { get; set; }

    public int DuplicateRows { get; set; }

    public int ImportedRecords { get; set; }

    public List<PamaProductionImportErrorDto> Errors { get; set; } = [];
}