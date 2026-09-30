namespace AutomotiveIntelligence.Application.Interfaces;

public interface IValuationRequestValidator
{
    IReadOnlyList<string> Validate(
        DTOs.ValuationRequestDto request);
}