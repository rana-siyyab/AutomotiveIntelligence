using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IUsedCarDatasetVehicleMappingService
{
    Task<UsedCarDatasetVehicleMappingResultDto> MapAsync(
        int sourceId,
        int batchSize = 500,
        CancellationToken cancellationToken = default);

    Task<UsedCarDatasetVehicleMappingResultDto> GetStatusAsync(
        int sourceId,
        CancellationToken cancellationToken = default);
}