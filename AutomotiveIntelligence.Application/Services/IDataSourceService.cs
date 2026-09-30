using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataSourceService
{
    Task<IReadOnlyList<DataSourceDto>> GetActiveAsync();

    Task<DataSourceDto?> GetByIdAsync(int sourceId);
    Task<DataSourceDto> CreateAsync(CreateDataSourceDto request);
    Task<IReadOnlyList<DataSourceDto>> GetAllAsync();

    Task<DataSourceDto?> SetActiveStatusAsync(
        int sourceId,
        bool isActive);
}