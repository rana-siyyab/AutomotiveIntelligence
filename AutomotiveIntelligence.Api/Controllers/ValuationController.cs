using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutomotiveIntelligence.Api.Controllers;

[ApiController]
[Route("api/v1/valuation")]
public class ValuationController : ControllerBase
{
    private readonly IValuationService _valuationService;

    public ValuationController(
        IValuationService valuationService)
    {
        _valuationService = valuationService;
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateAsync(
     [FromBody] ValuationRequestDto request)
    {
        try
        {
            var result =
                await _valuationService.CalculateValuationAsync(request);

            return Ok(result);
        }
        catch (Application.Exceptions.ValidationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message,
                errors = ex.Errors
            });
        }
    }

    [HttpGet("{valuationId:long}")]
    public async Task<IActionResult> GetByIdAsync(long valuationId)
    {
        var valuation =
            await _valuationService.GetValuationByIdAsync(valuationId);

        if (valuation is null)
        {
            return NotFound(new
            {
                message = $"Valuation with ID {valuationId} was not found."
            });
        }

        return Ok(valuation);
    }
    [HttpGet("recent")]
    public async Task<IActionResult> GetRecentAsync(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        if (pageNumber < 1)
            pageNumber = 1;

        if (pageSize < 1)
            pageSize = 10;

        if (pageSize > 100)
            pageSize = 100;

        var valuations =
            await _valuationService.GetRecentAsync(
                pageNumber,
                pageSize);

        return Ok(valuations);
    }
}