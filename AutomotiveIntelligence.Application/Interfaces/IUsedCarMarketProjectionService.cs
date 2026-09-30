using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IUsedCarMarketProjectionService
{
    Task<UsedCarMarketProjectionResultDto> ProjectAsync(
        int sourceId,
        int batchSize = 500,
        CancellationToken cancellationToken = default);

    Task<UsedCarMarketProjectionResultDto> GetStatusAsync(
        int sourceId,
        CancellationToken cancellationToken = default);
}