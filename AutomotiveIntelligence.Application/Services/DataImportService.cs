using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Application.Services;

public class DataImportService : IDataImportService
{
    private readonly IDataImportRepository _repository;

    private readonly IDataSourceRepository _dataSourceRepository;

    public DataImportService(
        IDataImportRepository repository,
        IDataSourceRepository dataSourceRepository)
    {
        _repository = repository;
        _dataSourceRepository = dataSourceRepository;
    }

    public async Task<IReadOnlyList<DataImportDto>>
        GetRecentAsync(
            int pageNumber,
            int pageSize)
    {
        var imports =
            await _repository.GetRecentAsync(
                pageNumber,
                pageSize);

        var sources =
            await _dataSourceRepository.GetActiveAsync();

        var sourceLookup = sources
            .ToDictionary(
                x => x.SourceId,
                x => new
                {
                    x.Name,
                    x.SourceType
                });

        return imports
            .Select(x =>
            {
                sourceLookup.TryGetValue(
                    x.SourceId,
                    out var source);

                return new DataImportDto
                {
                    ImportId = x.ImportId,
                    SourceId = x.SourceId,
                    SourceName =
                        source?.Name ?? "Unknown Source",
                    SourceType =
                        source?.SourceType ?? "Unknown",
                    FileName = x.FileName,
                    RecordsProcessed =
                        x.RecordsProcessed,
                    RecordsInserted =
                        x.RecordsInserted,
                    RecordsRejected =
                        x.RecordsRejected,
                    RecordsDuplicated =
    x.RecordsDuplicated,
                    ImportStatus =
                        x.ImportStatus,
                    StartedDate =
                        x.StartedDate,
                    CompletedDate =
                        x.CompletedDate
                };
            })
            .ToList();
    }

    public async Task<DataImportDto?>
        GetByIdAsync(long importId)
    {
        var import =
            await _repository.GetByIdAsync(importId);

        if (import == null)
        {
            return null;
        }

        var source =
            await _dataSourceRepository
                .GetByIdAsync(import.SourceId);

        return new DataImportDto
        {
            ImportId = import.ImportId,
            SourceId = import.SourceId,
            SourceName =
                source?.Name ?? "Unknown Source",
            SourceType =
                source?.SourceType ?? "Unknown",
            FileName = import.FileName,
            RecordsProcessed =
                import.RecordsProcessed,
            RecordsInserted =
                import.RecordsInserted,
            RecordsRejected =
                import.RecordsRejected,
            RecordsDuplicated =
    import.RecordsDuplicated,
            ImportStatus =
                import.ImportStatus,
            StartedDate =
                import.StartedDate,
            CompletedDate =
                import.CompletedDate
        };
    }
}