using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataQualityBySourceRepository
{
    Task<IReadOnlyList<DataQualityBySourceDto>>
        GetBySourceAsync();
}