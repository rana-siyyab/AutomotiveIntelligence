using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataImportService
{
    Task<IReadOnlyList<DataImportDto>> GetRecentAsync(
        int pageNumber,
        int pageSize);

    Task<DataImportDto?> GetByIdAsync(
        long importId);
}