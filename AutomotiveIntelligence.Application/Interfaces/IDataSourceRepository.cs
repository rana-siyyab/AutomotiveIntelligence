using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataSourceRepository
{
    Task<IReadOnlyList<DataSource>> GetActiveAsync();

    Task<DataSource?> GetByIdAsync(int sourceId);
    Task<DataSource> CreateAsync(DataSource source);
    Task<bool> ExistsByNameAsync(string name);
    Task<IReadOnlyList<DataSource>> GetAllAsync();

    Task<bool> SetActiveStatusAsync(
        int sourceId,
        bool isActive);
}