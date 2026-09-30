using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IPamaIntelligenceRepository
{
    Task<IReadOnlyList<PamaProduction>> GetAllAsync(
        int? fromYear = null,
        int? toYear = null,
        int? makeId = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default);
}