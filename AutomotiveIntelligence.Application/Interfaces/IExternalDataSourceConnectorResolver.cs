using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IExternalDataSourceConnectorResolver
{
    IExternalDataSourceConnector Resolve(
        DataSource source);
}