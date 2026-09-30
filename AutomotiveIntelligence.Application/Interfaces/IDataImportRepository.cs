using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataImportRepository
{
    Task<IReadOnlyList<DataImport>> GetRecentAsync(
        int pageNumber,
        int pageSize);

    Task<DataImport?> GetByIdAsync(long importId);
}