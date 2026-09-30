namespace AutomotiveIntelligence.Application.DTOs;

public class OicaProductionImportErrorDto
{
    public int RowNumber { get; set; }

    public string ErrorType { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;
}