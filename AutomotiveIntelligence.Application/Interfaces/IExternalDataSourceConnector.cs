using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IExternalDataSourceConnector
{
    bool CanHandle(DataSource source);

    Task<ExternalDataFetchResultDto> FetchAsync(
        DataSource source,
        CancellationToken cancellationToken = default);
}