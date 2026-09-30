using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/data-sources")]
public class DataSourceController : ControllerBase
{
    private readonly IDataSourceService _service;

    public DataSourceController(
        IDataSourceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetActive()
    {
        var sources =
            await _service.GetActiveAsync();

        return Ok(sources);
    }

    [HttpGet("{sourceId:int}")]
    public async Task<IActionResult> GetById(
        int sourceId)
    {
        var source =
            await _service.GetByIdAsync(sourceId);

        if (source == null)
        {
            return NotFound();
        }

        return Ok(source);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateDataSourceDto request)
    {
        try
        {
            var result = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { sourceId = result.SourceId },
                result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new
            {
                message = ex.Message
            });
        }
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var sources = await _service.GetAllAsync();

        return Ok(sources);
    }
    [HttpPut("{sourceId:int}/status")]
    public async Task<IActionResult> SetActiveStatus(
    int sourceId,
    [FromBody] bool isActive)
    {
        var result =
            await _service.SetActiveStatusAsync(
                sourceId,
                isActive);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}