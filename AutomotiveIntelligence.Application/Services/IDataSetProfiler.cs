using AutomotiveIntelligence.Application.DTOs.DataProfiling;

namespace AutomotiveIntelligence.Application.Services;

public interface IDataSetProfiler
{
    Task<DatasetProfileDto> ProfileCsvAsync(
        Stream csvStream,
        string fileName,
        CancellationToken cancellationToken = default);
}