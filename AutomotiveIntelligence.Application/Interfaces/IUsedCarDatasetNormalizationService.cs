using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IUsedCarDatasetNormalizationService
{
    Task<UsedCarDatasetNormalizationResultDto>
        NormalizeAsync(
            int sourceId,
            int batchSize = 1000,
            CancellationToken cancellationToken = default);

    Task<UsedCarDatasetNormalizationResultDto>
        GetStatusAsync(
            int sourceId,
            CancellationToken cancellationToken = default);
}