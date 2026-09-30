using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IPamaProductionRepository
{
    Task<IReadOnlyList<PamaProduction>> GetAsync(
        int? year = null,
        int? makeId = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default);
}