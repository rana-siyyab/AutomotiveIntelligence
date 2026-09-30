using AutomotiveIntelligence.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/data-quality")]
public class DataQualityBySourceController : ControllerBase
{
    private readonly DataQualityBySourceService _service;

    public DataQualityBySourceController(
        DataQualityBySourceService service)
    {
        _service = service;
    }

    [HttpGet("by-source")]
    public async Task<IActionResult> GetBySource()
    {
        var result = await _service.GetBySourceAsync();

        return Ok(result);
    }
}