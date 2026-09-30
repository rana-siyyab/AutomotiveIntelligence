using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Services;

public class DataSourceService : IDataSourceService
{
    private readonly IDataSourceRepository _repository;

    public DataSourceService(
        IDataSourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DataSourceDto>>
        GetActiveAsync()
    {
        var sources =
            await _repository.GetActiveAsync();

        return sources
     .Select(x => new DataSourceDto
     {
         SourceId = x.SourceId,
         Name = x.Name,
         SourceType = x.SourceType,
         BaseUrl = x.BaseUrl,
         IsActive = x.IsActive,

         License = x.License,
         CommercialUseAllowed = x.CommercialUseAllowed,
         CommercialTrainingAllowed =
             x.CommercialTrainingAllowed,
         AttributionRequired =
             x.AttributionRequired,
         TermsUrl = x.TermsUrl,
         AcquiredDate = x.AcquiredDate
     })
     .ToList();
    }

    public async Task<DataSourceDto?>
        GetByIdAsync(int sourceId)
    {
        var source =
            await _repository.GetByIdAsync(sourceId);

        if (source == null)
        {
            return null;
        }

        return new DataSourceDto
        {
            SourceId = source.SourceId,
            Name = source.Name,
            SourceType = source.SourceType,
            BaseUrl = source.BaseUrl,
            IsActive = source.IsActive
        };
    }

    public async Task<DataSourceDto> CreateAsync(
        CreateDataSourceDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException(
                "Data source name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.SourceType))
        {
            throw new ArgumentException(
                "Source type is required.");
        }

        var name = request.Name.Trim();
        var sourceType = request.SourceType.Trim();

        var exists =
            await _repository.ExistsByNameAsync(name);

        if (exists)
        {
            throw new InvalidOperationException(
                $"A data source named '{name}' already exists.");
        }

        var source = new DataSource
        {
            Name = request.Name.Trim(),

            SourceType = request.SourceType.Trim(),

            BaseUrl = string.IsNullOrWhiteSpace(request.BaseUrl)
        ? null
        : request.BaseUrl.Trim(),

            IsActive = request.IsActive,

            License = string.IsNullOrWhiteSpace(request.License)
        ? null
        : request.License.Trim(),

            CommercialUseAllowed =
        request.CommercialUseAllowed,

            CommercialTrainingAllowed =
        request.CommercialTrainingAllowed,

            AttributionRequired =
        request.AttributionRequired,

            TermsUrl = string.IsNullOrWhiteSpace(request.TermsUrl)
        ? null
        : request.TermsUrl.Trim(),

            AcquiredDate =
        request.AcquiredDate,

            CreatedDate = DateTime.UtcNow
        };

        var createdSource =
            await _repository.CreateAsync(source);

        return new DataSourceDto
        {
            SourceId = createdSource.SourceId,
            Name = createdSource.Name,
            SourceType = createdSource.SourceType,
            BaseUrl = createdSource.BaseUrl,
            IsActive = createdSource.IsActive,

            License = createdSource.License,
            CommercialUseAllowed =
         createdSource.CommercialUseAllowed,
            CommercialTrainingAllowed =
         createdSource.CommercialTrainingAllowed,
            AttributionRequired =
         createdSource.AttributionRequired,
            TermsUrl = createdSource.TermsUrl,
            AcquiredDate =
         createdSource.AcquiredDate
        };
    }

    public async Task<IReadOnlyList<DataSourceDto>> GetAllAsync()
    {
        var sources =
            await _repository.GetAllAsync();

        return sources
            .Select(x => new DataSourceDto
            {
                SourceId = x.SourceId,
                Name = x.Name,
                SourceType = x.SourceType,
                BaseUrl = x.BaseUrl,
                IsActive = x.IsActive
            })
            .ToList();
    }
    public async Task<DataSourceDto?> SetActiveStatusAsync(
    int sourceId,
    bool isActive)
    {
        var updated =
            await _repository.SetActiveStatusAsync(
                sourceId,
                isActive);

        if (!updated)
        {
            return null;
        }

        var sources =
            await _repository.GetAllAsync();

        var source =
            sources.FirstOrDefault(
                x => x.SourceId == sourceId);

        if (source == null)
        {
            return null;
        }

        return new DataSourceDto
        {
            SourceId = source.SourceId,
            Name = source.Name,
            SourceType = source.SourceType,
            BaseUrl = source.BaseUrl,
            IsActive = source.IsActive
        };
    }
}