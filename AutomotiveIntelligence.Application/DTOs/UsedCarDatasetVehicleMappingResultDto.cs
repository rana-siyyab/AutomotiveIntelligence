namespace AutomotiveIntelligence.Application.DTOs;

public class UsedCarDatasetVehicleMappingResultDto
{
    public int SourceId { get; set; }

    public int ProcessedRecords { get; set; }

    public int MappedRecords { get; set; }

    public int PendingRecords { get; set; }

    public int MakeNotFoundRecords { get; set; }

    public int ModelNotFoundRecords { get; set; }

    public int VariantNotFoundRecords { get; set; }

    public int ExactVariantMatches { get; set; }

    public int SingleVariantModelMatches { get; set; }

    public int DuplicateRecords { get; set; }

    public string Status { get; set; } = string.Empty;
}