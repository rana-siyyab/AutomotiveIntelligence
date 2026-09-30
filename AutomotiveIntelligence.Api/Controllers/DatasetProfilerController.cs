using AutomotiveIntelligence.Application.DTOs.DataProfiling;
using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/dataset-profiler")]
public class DatasetProfilerController : ControllerBase
{
    private readonly IDataSetProfiler _profiler;

    public DatasetProfilerController(
        IDataSetProfiler profiler)
    {
        _profiler = profiler;
    }

    [HttpPost("csv")]
    [RequestSizeLimit(100_000_000)]
    public async Task<ActionResult<DatasetProfileDto>> ProfileCsv(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(
                "A CSV file is required.");
        }

        await using var stream = file.OpenReadStream();

        var result =
            await _profiler.ProfileCsvAsync(
                stream,
                file.FileName,
                cancellationToken);

        return Ok(result);
    }
}