using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class DataQualityBySourceService
{
    private readonly IDataQualityBySourceRepository _repository;

    public DataQualityBySourceService(
        IDataQualityBySourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DataQualityBySourceDto>>
        GetBySourceAsync()
    {
        return await _repository.GetBySourceAsync();
    }
}