namespace AutomotiveIntelligence.Application.Interfaces;

public interface IDataQualityRepository
{
    Task<int> GetTotalImportsAsync();

    Task<int> GetTotalRecordsProcessedAsync();

    Task<int> GetTotalRecordsInsertedAsync();

    Task<int> GetTotalRecordsRejectedAsync();

    Task<int> GetTotalRecordsDuplicatedAsync();

    Task<int> GetSuccessfulImportsAsync();

    Task<int> GetImportsWithErrorsAsync();

    Task<DateTime?> GetLastImportDateAsync();
}