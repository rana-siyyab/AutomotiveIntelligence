using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Infrastructure.Services;

public class OicaDataSourceConnector
    : IExternalDataSourceConnector
{
    public bool CanHandle(DataSource source)
    {
        return source.SourceType.Equals(
            "OICA",
            StringComparison.OrdinalIgnoreCase);
    }

    public async Task<ExternalDataFetchResultDto> FetchAsync(
        DataSource source,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(source.BaseUrl))
        {
            return new ExternalDataFetchResultDto
            {
                Success = false,
                SourceId = source.SourceId,
                SourceName = source.Name,
                RetrievedDate = DateTime.UtcNow,
                ErrorMessage =
                    "No approved OICA data file location has been configured."
            };
        }

        var filePath = source.BaseUrl;

        if (!File.Exists(filePath))
        {
            return new ExternalDataFetchResultDto
            {
                Success = false,
                SourceId = source.SourceId,
                SourceName = source.Name,
                RetrievedDate = DateTime.UtcNow,
                ErrorMessage =
                    $"OICA data file was not found: {filePath}"
            };
        }

        var memoryStream = new MemoryStream();

        await using (var fileStream = new FileStream(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read))
        {
            await fileStream.CopyToAsync(
                memoryStream,
                cancellationToken);
        }

        memoryStream.Position = 0;

        return new ExternalDataFetchResultDto
        {
            Success = true,
            SourceId = source.SourceId,
            SourceName = source.Name,
            FileName = Path.GetFileName(filePath),
            ContentType = "text/csv",
            Data = memoryStream,
            RetrievedDate = DateTime.UtcNow
        };
    }
}