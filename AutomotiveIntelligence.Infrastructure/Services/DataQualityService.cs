using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class DataQualityService : IDataQualityService
{
    private readonly IDataQualityRepository _repository;

    public DataQualityService(
        IDataQualityRepository repository)
    {
        _repository = repository;
    }

    public async Task<DataQualityDto> GetOverviewAsync()
    {
        var totalImports =
            await _repository.GetTotalImportsAsync();

        var totalRecordsProcessed =
            await _repository.GetTotalRecordsProcessedAsync();

        var totalRecordsInserted =
            await _repository.GetTotalRecordsInsertedAsync();

        var totalRecordsRejected =
            await _repository.GetTotalRecordsRejectedAsync();

        var totalRecordsDuplicated =
            await _repository.GetTotalRecordsDuplicatedAsync();

        var successfulImports =
            await _repository.GetSuccessfulImportsAsync();

        var importsWithErrors =
            await _repository.GetImportsWithErrorsAsync();

        var lastImportDate =
            await _repository.GetLastImportDateAsync();

        decimal rejectionRate = 0;
        decimal duplicateRate = 0;

        if (totalRecordsProcessed > 0)
        {
            rejectionRate =
                Math.Round(
                    totalRecordsRejected * 100m /
                    totalRecordsProcessed,
                    2);

            duplicateRate =
                Math.Round(
                    totalRecordsDuplicated * 100m /
                    totalRecordsProcessed,
                    2);
        }

        return new DataQualityDto
        {
            TotalImports = totalImports,
            TotalRecordsProcessed = totalRecordsProcessed,
            TotalRecordsInserted = totalRecordsInserted,
            TotalRecordsRejected = totalRecordsRejected,
            TotalRecordsDuplicated = totalRecordsDuplicated,
            RejectionRate = rejectionRate,
            DuplicateRate = duplicateRate,
            SuccessfulImports = successfulImports,
            ImportsWithErrors = importsWithErrors,
            LastImportDate = lastImportDate
        };
    }
}