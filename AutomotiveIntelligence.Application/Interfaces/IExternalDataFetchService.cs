using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IExternalDataFetchService
{
    Task<ExternalDataFetchResultDto> FetchAsync(
        int sourceId,
        CancellationToken cancellationToken = default);
}