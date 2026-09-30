using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IOicaProductionRepository
{
    Task<IReadOnlyList<OicaProduction>> GetAsync(
        int? fromYear = null,
        int? toYear = null,
        string? countryName = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default);
}