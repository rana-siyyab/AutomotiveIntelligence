using AutomotiveIntelligence.Application.DTOs.UsedCarDataset;

namespace AutomotiveIntelligence.Application.Services;

public interface IUsedCarDatasetImportService
{
    Task<UsedCarDatasetImportResultDto> ImportAsync(
        Stream csvStream,
        string fileName,
        int sourceId,
        CancellationToken cancellationToken = default);
}