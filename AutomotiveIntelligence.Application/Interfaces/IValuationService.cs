using AutomotiveIntelligence.Application.DTOs;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IValuationService
{
    Task<ValuationResultDto> CalculateValuationAsync(
        ValuationRequestDto request);

    Task<ValuationResultDto?> GetValuationByIdAsync(
        long valuationId);
    Task<IReadOnlyList<ValuationResultDto>> GetRecentAsync(
    int pageNumber,
    int pageSize);
}