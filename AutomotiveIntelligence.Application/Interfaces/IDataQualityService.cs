using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataQualityService
{
    Task<DataQualityDto> GetOverviewAsync();
}