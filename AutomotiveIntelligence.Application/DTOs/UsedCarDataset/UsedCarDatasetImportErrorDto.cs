namespace AutomotiveIntelligence.Application.DTOs.UsedCarDataset;

public class UsedCarDatasetImportErrorDto
{
    public int RowNumber { get; set; }

    public string ErrorType { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;
}