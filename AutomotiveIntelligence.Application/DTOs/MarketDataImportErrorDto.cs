namespace AutomotiveIntelligence.Application.DTOs;

public class MarketDataImportErrorDto
{
    public int RowNumber { get; set; }

    public string ErrorType { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;
}