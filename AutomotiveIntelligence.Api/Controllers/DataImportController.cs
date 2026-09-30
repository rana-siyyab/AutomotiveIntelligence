using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/data-imports")]
public class DataImportController : ControllerBase
{
    private readonly IDataImportService _service;

    public DataImportController(
        IDataImportService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecent(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        pageNumber = Math.Max(1, pageNumber);

        pageSize = Math.Clamp(
            pageSize,
            1,
            100);

        var imports =
            await _service.GetRecentAsync(
                pageNumber,
                pageSize);

        return Ok(imports);
    }

    [HttpGet("{importId:long}")]
    public async Task<IActionResult> GetById(
        long importId)
    {
        var import =
            await _service.GetByIdAsync(
                importId);

        if (import == null)
        {
            return NotFound();
        }

        return Ok(import);
    }
}