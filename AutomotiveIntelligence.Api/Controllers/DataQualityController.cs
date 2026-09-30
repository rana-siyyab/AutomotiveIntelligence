using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/data-quality")]
public class DataQualityController : ControllerBase
{
    private readonly IDataQualityService _service;

    public DataQualityController(
        IDataQualityService service)
    {
        _service = service;
    }

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        var result = await _service.GetOverviewAsync();

        return Ok(result);
    }
}