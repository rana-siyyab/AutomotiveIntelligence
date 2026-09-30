using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class ExternalDataFetchService : IExternalDataFetchService
{
    private readonly IDataSourceRepository _dataSourceRepository;
    private readonly IExternalDataSourceConnectorResolver _resolver;

    public ExternalDataFetchService(
        IDataSourceRepository dataSourceRepository,
        IExternalDataSourceConnectorResolver resolver)
    {
        _dataSourceRepository = dataSourceRepository;
        _resolver = resolver;
    }

    public async Task<ExternalDataFetchResultDto> FetchAsync(
        int sourceId,
        CancellationToken cancellationToken = default)
    {
        var source = await _dataSourceRepository
            .GetByIdAsync(sourceId);

        if (source == null)
        {
            return new ExternalDataFetchResultDto
            {
                Success = false,
                SourceId = sourceId,
                ErrorMessage =
                    $"Data source with ID {sourceId} was not found or is inactive.",
                RetrievedDate = DateTime.UtcNow
            };
        }

        var connector = _resolver.Resolve(source);

        return await connector.FetchAsync(
            source,
            cancellationToken);
    }
}