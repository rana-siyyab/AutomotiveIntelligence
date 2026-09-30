using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Services;

public class RuleBasedValuationService : IValuationService
{
    private readonly IMarketPriceRepository _marketPriceRepository;
    private readonly IVehicleConditionRepository _vehicleConditionRepository;
    private readonly IValuationRepository _valuationRepository;
    private readonly IValuationRequestValidator _validator;

    public RuleBasedValuationService(
        IMarketPriceRepository marketPriceRepository,
        IVehicleConditionRepository vehicleConditionRepository,
        IValuationRepository valuationRepository,
        IValuationRequestValidator validator)
    {
        _marketPriceRepository = marketPriceRepository;
        _vehicleConditionRepository = vehicleConditionRepository;
        _valuationRepository = valuationRepository;
        _validator = validator;
    }

    public async Task<ValuationResultDto> CalculateValuationAsync(
        ValuationRequestDto request)
    {
        var validationErrors = _validator.Validate(request);

        if (validationErrors.Count > 0)
        {
            throw new Exceptions.ValidationException(
                validationErrors);
        }

        /*
         * The repository supplies the comparable market observations
         * based on the requested make/model/variant/year/city.
         *
         * The repository abstraction remains unchanged.
         */
        var marketPrices =
            await _marketPriceRepository.GetMarketPricesAsync(
                request.MakeId,
                request.ModelId,
                request.VariantId,
                request.ManufacturingYear,
                request.CityId);

        if (marketPrices.Count == 0)
        {
            return new ValuationResultDto
            {
                EstimatedPrice = 0,
                MinimumPrice = 0,
                MaximumPrice = 0,
                ConfidenceScore = 0,
                DealAssessment = "Insufficient Market Data",
                ValuationMethod = "RuleBasedMarketData"
            };
        }

        /*
         * =========================================================
         * Market baseline
         * =========================================================
         *
         * Use a trimmed market sample when enough observations
         * are available. This reduces the influence of extreme
         * asking prices while retaining the actual market data.
         */
        var marketPricesOrdered = marketPrices
            .Where(x => x.ObservedPrice > 0)
            .OrderBy(x => x.ObservedPrice)
            .ToList();

        if (marketPricesOrdered.Count == 0)
        {
            return new ValuationResultDto
            {
                EstimatedPrice = 0,
                MinimumPrice = 0,
                MaximumPrice = 0,
                ConfidenceScore = 0,
                DealAssessment = "Insufficient Market Data",
                ValuationMethod = "RuleBasedMarketData"
            };
        }

        var baselinePrice =
            CalculateMarketBaseline(
                marketPricesOrdered);

        var estimatedPrice = baselinePrice;

        var factors = new List<ValuationFactorDto>();

        /*
         * =========================================================
         * Mileage adjustment
         * =========================================================
         */

        if (request.Mileage.HasValue)
        {
            decimal mileagePercentage = 0;

            if (request.Mileage.Value <= 30000)
            {
                mileagePercentage = 0.05m;
            }
            else if (request.Mileage.Value >= 100000)
            {
                mileagePercentage = -0.05m;
            }

            var adjustment =
                estimatedPrice * mileagePercentage;

            estimatedPrice += adjustment;

            factors.Add(new ValuationFactorDto
            {
                FactorType = "Mileage",
                FactorName = "Mileage Adjustment",
                AdjustmentValue = adjustment,
                AdjustmentPercentage =
                    mileagePercentage * 100,
                Description =
                    request.Mileage.Value <= 30000
                        ? "Lower mileage adjustment applied."
                        : request.Mileage.Value >= 100000
                            ? "High mileage adjustment applied."
                            : "No significant mileage adjustment."
            });
        }

        /*
         * =========================================================
         * Accident history adjustment
         * =========================================================
         */

        if (request.HasAccidentHistory == true)
        {
            var adjustment =
                estimatedPrice * -0.10m;

            estimatedPrice += adjustment;

            factors.Add(new ValuationFactorDto
            {
                FactorType = "AccidentHistory",
                FactorName = "Accident History",
                AdjustmentValue = adjustment,
                AdjustmentPercentage = -10,
                Description =
                    "Adjustment applied because accident history was reported."
            });
        }

        /*
         * =========================================================
         * Owner count adjustment
         * =========================================================
         */

        if (request.OwnerCount.HasValue &&
            request.OwnerCount.Value >= 3)
        {
            var adjustment =
                estimatedPrice * -0.03m;

            estimatedPrice += adjustment;

            factors.Add(new ValuationFactorDto
            {
                FactorType = "Ownership",
                FactorName = "Multiple Previous Owners",
                AdjustmentValue = adjustment,
                AdjustmentPercentage = -3,
                Description =
                    "Adjustment applied for three or more previous owners."
            });
        }

        /*
         * =========================================================
         * Condition adjustment
         * =========================================================
         */

        if (request.ConditionId.HasValue)
        {
            var condition =
                await _vehicleConditionRepository
                    .GetByIdAsync(request.ConditionId.Value);

            if (condition is not null)
            {
                decimal conditionPercentage =
                    condition.Score switch
                    {
                        >= 90 => 0.05m,
                        >= 80 => 0.02m,
                        >= 65 => 0m,
                        >= 50 => -0.04m,
                        _ => -0.08m
                    };

                var adjustment =
                    estimatedPrice * conditionPercentage;

                estimatedPrice += adjustment;

                factors.Add(new ValuationFactorDto
                {
                    FactorType = "Condition",
                    FactorName = condition.Name,
                    AdjustmentValue = adjustment,
                    AdjustmentPercentage =
                        conditionPercentage * 100,
                    Description =
                        condition.Description
                });
            }
        }

        estimatedPrice =
            Math.Max(0, estimatedPrice);

        /*
         * =========================================================
         * Information level
         * =========================================================
         */

        var informationLevel =
            CalculateInformationLevel(request);

        /*
         * =========================================================
         * Market range
         *
         * More complete vehicle information produces a narrower
         * range.
         * =========================================================
         */

        var rangePercentage =
            informationLevel switch
            {
                ValuationInformationLevel.Detailed => 0.10m,
                ValuationInformationLevel.Standard => 0.15m,
                ValuationInformationLevel.Basic => 0.20m,
                _ => 0.25m
            };

        /*
         * If we only have a very small comparable sample,
         * widen the range slightly.
         */
        if (marketPricesOrdered.Count < 5)
        {
            rangePercentage =
                Math.Max(
                    rangePercentage,
                    0.20m);
        }

        var minimumPrice =
            estimatedPrice * (1 - rangePercentage);

        var maximumPrice =
            estimatedPrice * (1 + rangePercentage);

        /*
         * =========================================================
         * Confidence
         * =========================================================
         */

        var confidenceScore =
            CalculateConfidenceScore(
                marketPricesOrdered.Count,
                informationLevel);

        /*
         * =========================================================
         * Deal assessment
         * =========================================================
         */

        string dealAssessment =
            "No Asking Price Provided";

        if (request.AskingPrice.HasValue)
        {
            dealAssessment =
                request.AskingPrice.Value < minimumPrice
                    ? "Potential Good Deal"
                    : request.AskingPrice.Value > maximumPrice
                        ? "Above Estimated Market Range"
                        : "Within Estimated Market Range";
        }

        /*
         * =========================================================
         * Recommended prices
         * =========================================================
         */

        decimal? recommendedBuyingPrice =
            request.AskingPrice.HasValue
                ? estimatedPrice * 0.97m
                : null;

        decimal? recommendedNegotiationPrice =
            request.AskingPrice.HasValue
                ? estimatedPrice * 0.93m
                : null;

        /*
         * =========================================================
         * Persistence
         * =========================================================
         */

        var canPersistDetailedValuation =
            request.VariantId.HasValue &&
            request.ManufacturingYear.HasValue &&
            request.CityId.HasValue;

        if (!canPersistDetailedValuation)
        {
            return new ValuationResultDto
            {
                ValuationId = 0,

                EstimatedPrice =
                    Math.Round(
                        estimatedPrice,
                        2),

                MinimumPrice =
                    Math.Round(
                        minimumPrice,
                        2),

                MaximumPrice =
                    Math.Round(
                        maximumPrice,
                        2),

                ConfidenceScore =
                    confidenceScore,

                DealAssessment =
                    dealAssessment,

                RecommendedBuyingPrice =
                    recommendedBuyingPrice.HasValue
                        ? Math.Round(
                            recommendedBuyingPrice.Value,
                            2)
                        : null,

                RecommendedNegotiationPrice =
                    recommendedNegotiationPrice.HasValue
                        ? Math.Round(
                            recommendedNegotiationPrice.Value,
                            2)
                        : null,

                ValuationMethod =
                    "RuleBasedMarketData",

                Factors =
                    factors
            };
        }

        /*
         * =========================================================
         * Persist detailed valuation
         * =========================================================
         */

        var valuation = new Valuation
        {
            VariantId =
                request.VariantId.Value,

            ManufacturingYear =
                request.ManufacturingYear.Value,

            Mileage =
                request.Mileage,

            CityId =
                request.CityId.Value,

            ConditionId =
                request.ConditionId,

            AskingPrice =
                request.AskingPrice,

            EstimatedPrice =
                Math.Round(
                    estimatedPrice,
                    2),

            MinimumPrice =
                Math.Round(
                    minimumPrice,
                    2),

            MaximumPrice =
                Math.Round(
                    maximumPrice,
                    2),

            ConfidenceScore =
                confidenceScore,

            ValuationMethod =
                "RuleBasedMarketData",

            CreatedDate =
                DateTime.UtcNow,

            DealAssessment =
                dealAssessment,

            RecommendedBuyingPrice =
                recommendedBuyingPrice.HasValue
                    ? Math.Round(
                        recommendedBuyingPrice.Value,
                        2)
                    : null,

            RecommendedNegotiationPrice =
                recommendedNegotiationPrice.HasValue
                    ? Math.Round(
                        recommendedNegotiationPrice.Value,
                        2)
                    : null
        };

        var valuationFactors =
            factors
                .Select(x => new ValuationFactor
                {
                    FactorType =
                        x.FactorType,

                    FactorName =
                        x.FactorName,

                    AdjustmentValue =
                        x.AdjustmentValue,

                    AdjustmentPercentage =
                        x.AdjustmentPercentage,

                    Description =
                        x.Description,

                    CreatedDate =
                        DateTime.UtcNow
                })
                .ToList();

        var savedValuation =
            await _valuationRepository.AddAsync(
                valuation,
                valuationFactors);

        return new ValuationResultDto
        {
            ValuationId =
                savedValuation.ValuationId,

            EstimatedPrice =
                Math.Round(
                    estimatedPrice,
                    2),

            MinimumPrice =
                Math.Round(
                    minimumPrice,
                    2),

            MaximumPrice =
                Math.Round(
                    maximumPrice,
                    2),

            ConfidenceScore =
                confidenceScore,

            DealAssessment =
                dealAssessment,

            RecommendedBuyingPrice =
                recommendedBuyingPrice.HasValue
                    ? Math.Round(
                        recommendedBuyingPrice.Value,
                        2)
                    : null,

            RecommendedNegotiationPrice =
                recommendedNegotiationPrice.HasValue
                    ? Math.Round(
                        recommendedNegotiationPrice.Value,
                        2)
                    : null,

            ValuationMethod =
                "RuleBasedMarketData",

            Factors =
                factors
        };
    }

    // =============================================================
    // Market baseline
    // =============================================================

    private static decimal CalculateMarketBaseline(
        IReadOnlyList<MarketPrice> marketPrices)
    {
        if (marketPrices.Count == 0)
        {
            return 0;
        }

        /*
         * Small samples:
         * use the median.
         */
        if (marketPrices.Count < 10)
        {
            return CalculateMedian(marketPrices);
        }

        /*
         * Larger samples:
         * trim the lowest and highest 10% before calculating
         * the average.
         *
         * This protects the valuation from extreme asking prices.
         */
        var trimCount =
            (int)Math.Floor(
                marketPrices.Count * 0.10);

        if (trimCount == 0 ||
            marketPrices.Count -
            (trimCount * 2) <= 0)
        {
            return CalculateMedian(marketPrices);
        }

        var trimmed =
            marketPrices
                .Skip(trimCount)
                .Take(
                    marketPrices.Count -
                    (trimCount * 2))
                .ToList();

        return trimmed.Average(
            x => x.ObservedPrice);
    }

    private static decimal CalculateMedian(
        IReadOnlyList<MarketPrice> marketPrices)
    {
        if (marketPrices.Count == 0)
        {
            return 0;
        }

        var middle =
            marketPrices.Count / 2;

        if (marketPrices.Count % 2 == 0)
        {
            return (
                marketPrices[middle - 1].ObservedPrice +
                marketPrices[middle].ObservedPrice
            ) / 2m;
        }

        return marketPrices[middle].ObservedPrice;
    }

    // =============================================================
    // Determine information level
    // =============================================================

    private static ValuationInformationLevel
        CalculateInformationLevel(
            ValuationRequestDto request)
    {
        var score = 0;

        if (request.MakeId > 0)
            score++;

        if (request.ModelId > 0)
            score++;

        if (request.ManufacturingYear.HasValue)
            score++;

        if (request.VariantId.HasValue)
            score++;

        if (request.CityId.HasValue)
            score++;

        if (request.Mileage.HasValue)
            score++;

        if (request.ConditionId.HasValue)
            score++;

        if (request.HasAccidentHistory.HasValue)
            score++;

        if (request.OwnerCount.HasValue)
            score++;

        return score switch
        {
            >= 7 => ValuationInformationLevel.Detailed,
            >= 3 => ValuationInformationLevel.Standard,
            _ => ValuationInformationLevel.Basic
        };
    }

    // =============================================================
    // Confidence calculation
    // =============================================================

    private static decimal CalculateConfidenceScore(
        int marketSampleCount,
        ValuationInformationLevel informationLevel)
    {
        var sampleConfidence =
            marketSampleCount switch
            {
                >= 30 => 95m,
                >= 20 => 90m,
                >= 10 => 80m,
                >= 5 => 70m,
                >= 3 => 60m,
                _ => 40m
            };

        var informationAdjustment =
            informationLevel switch
            {
                ValuationInformationLevel.Detailed => 0m,
                ValuationInformationLevel.Standard => -5m,
                ValuationInformationLevel.Basic => -15m,
                _ => -15m
            };

        return Math.Clamp(
            sampleConfidence +
            informationAdjustment,
            0m,
            100m);
    }

    // =============================================================
    // Get valuation by ID
    // =============================================================

    public async Task<ValuationResultDto?> GetValuationByIdAsync(
        long valuationId)
    {
        var valuation =
            await _valuationRepository
                .GetByIdAsync(valuationId);

        if (valuation is null)
        {
            return null;
        }

        var factors =
            await _valuationRepository
                .GetFactorsByValuationIdAsync(
                    valuationId);

        return new ValuationResultDto
        {
            ValuationId =
                valuation.ValuationId,

            EstimatedPrice =
                valuation.EstimatedPrice,

            MinimumPrice =
                valuation.MinimumPrice,

            MaximumPrice =
                valuation.MaximumPrice,

            ConfidenceScore =
                valuation.ConfidenceScore,

            DealAssessment =
                valuation.DealAssessment ??
                string.Empty,

            RecommendedBuyingPrice =
                valuation.RecommendedBuyingPrice,

            RecommendedNegotiationPrice =
                valuation.RecommendedNegotiationPrice,

            ValuationMethod =
                valuation.ValuationMethod ??
                string.Empty,

            Factors =
                factors
                    .Select(x => new ValuationFactorDto
                    {
                        FactorType =
                            x.FactorType,

                        FactorName =
                            x.FactorName,

                        AdjustmentValue =
                            x.AdjustmentValue,

                        AdjustmentPercentage =
                            x.AdjustmentPercentage,

                        Description =
                            x.Description
                    })
                    .ToList()
        };
    }

    // =============================================================
    // Recent valuations
    // =============================================================

    public async Task<IReadOnlyList<ValuationResultDto>> GetRecentAsync(
        int pageNumber,
        int pageSize)
    {
        var valuations =
            await _valuationRepository.GetRecentAsync(
                pageNumber,
                pageSize);

        var results =
            new List<ValuationResultDto>();

        foreach (var valuation in valuations)
        {
            var factors =
                await _valuationRepository
                    .GetFactorsByValuationIdAsync(
                        valuation.ValuationId);

            results.Add(new ValuationResultDto
            {
                ValuationId =
                    valuation.ValuationId,

                EstimatedPrice =
                    valuation.EstimatedPrice,

                MinimumPrice =
                    valuation.MinimumPrice,

                MaximumPrice =
                    valuation.MaximumPrice,

                ConfidenceScore =
                    valuation.ConfidenceScore,

                DealAssessment =
                    valuation.DealAssessment ??
                    string.Empty,

                RecommendedBuyingPrice =
                    valuation.RecommendedBuyingPrice,

                RecommendedNegotiationPrice =
                    valuation.RecommendedNegotiationPrice,

                ValuationMethod =
                    valuation.ValuationMethod ??
                    string.Empty,

                Factors =
                    factors
                        .Select(x => new ValuationFactorDto
                        {
                            FactorType =
                                x.FactorType,

                            FactorName =
                                x.FactorName,

                            AdjustmentValue =
                                x.AdjustmentValue,

                            AdjustmentPercentage =
                                x.AdjustmentPercentage,

                            Description =
                                x.Description
                        })
                        .ToList()
            });
        }

        return results;
    }

    private enum ValuationInformationLevel
    {
        Basic,
        Standard,
        Detailed
    }
}