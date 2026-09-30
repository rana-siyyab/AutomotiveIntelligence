using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class PamaProductionService
{
    private readonly IPamaProductionRepository _repository;

    public PamaProductionService(
        IPamaProductionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PamaProductionDto>> GetAsync(
        int? year = null,
        int? makeId = null,
        string? vehicleType = null,
        CancellationToken cancellationToken = default)
    {
        var records = await _repository.GetAsync(
            year,
            makeId,
            vehicleType,
            cancellationToken);

        return records
            .Select(x => new PamaProductionDto
            {
                PamaProductionId = x.PamaProductionId,
                SourceId = x.SourceId,
                ImportId = x.ImportId,
                MakeId = x.MakeId,
                ManufacturerName = x.ManufacturerName,
                VehicleType = x.VehicleType,
                Year = x.Year,
                Month = x.Month,
                ProductionUnits = x.ProductionUnits,
                SalesUnits = x.SalesUnits,
                ObservationDate = x.ObservationDate
            })
            .ToList();
    }
}