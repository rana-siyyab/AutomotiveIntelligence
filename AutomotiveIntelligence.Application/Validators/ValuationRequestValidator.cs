using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
namespace AutomotiveIntelligence.Application.Validators;

public class ValuationRequestValidator
    : IValuationRequestValidator
{
    public IReadOnlyList<string> Validate(
        ValuationRequestDto request)
    {
        var errors = new List<string>();

        if (request.VariantId <= 0)
        {
            errors.Add("VariantId must be greater than zero.");
        }

        if (request.ManufacturingYear < 1980 ||
            request.ManufacturingYear > DateTime.UtcNow.Year + 1)
        {
            errors.Add("ManufacturingYear is outside the allowed range.");
        }

        if (request.Mileage.HasValue &&
            request.Mileage.Value < 0)
        {
            errors.Add("Mileage cannot be negative.");
        }

        if (request.CityId <= 0)
        {
            errors.Add("CityId must be greater than zero.");
        }

        if (request.ConditionId.HasValue &&
            request.ConditionId.Value <= 0)
        {
            errors.Add("ConditionId must be greater than zero.");
        }

        if (request.AskingPrice.HasValue &&
            request.AskingPrice.Value < 0)
        {
            errors.Add("AskingPrice cannot be negative.");
        }

        if (request.OwnerCount.HasValue &&
            request.OwnerCount.Value < 0)
        {
            errors.Add("OwnerCount cannot be negative.");
        }

        return errors;
    }
}