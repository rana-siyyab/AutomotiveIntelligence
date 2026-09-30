using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IPamaProductionImportService
{
    Task<PamaProductionImportResultDto> ImportAsync(
        Stream dataStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default);
}