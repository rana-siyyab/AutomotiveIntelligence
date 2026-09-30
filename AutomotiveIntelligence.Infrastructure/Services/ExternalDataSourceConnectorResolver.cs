using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class ExternalDataSourceConnectorResolver
    : IExternalDataSourceConnectorResolver
{
    private readonly IEnumerable<IExternalDataSourceConnector>
        _connectors;

    public ExternalDataSourceConnectorResolver(
        IEnumerable<IExternalDataSourceConnector> connectors)
    {
        _connectors = connectors;
    }

    public IExternalDataSourceConnector Resolve(
        DataSource source)
    {
        var connector = _connectors
            .FirstOrDefault(x => x.CanHandle(source));

        if (connector == null)
        {
            throw new InvalidOperationException(
                $"No external data connector is registered " +
                $"for data source '{source.Name}'.");
        }

        return connector;
    }
}