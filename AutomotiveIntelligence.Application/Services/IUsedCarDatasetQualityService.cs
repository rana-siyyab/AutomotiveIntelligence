using AutomotiveIntelligence.Application.DTOs.UsedCarDataset;

namespace AutomotiveIntelligence.Application.Services;

public interface IUsedCarDatasetQualityService
{
    Task<UsedCarDatasetQualityDto> GetQualityAsync(
        int sourceId,
        CancellationToken cancellationToken = default);
}