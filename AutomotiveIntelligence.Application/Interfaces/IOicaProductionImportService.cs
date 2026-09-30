using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IOicaProductionImportService
{
    Task<OicaProductionImportResultDto> ImportAsync(
        Stream dataStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default);
}