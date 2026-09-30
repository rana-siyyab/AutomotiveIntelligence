using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IMarketDataImportService
{
    Task<MarketDataImportResultDto> ImportAsync(
        Stream csvStream,
        string fileName,
        int sourceId);
}