namespace AutomotiveIntelligence.Application.DTOs.UsedCarDataset;

public class UsedCarDatasetQualityDto
{
    public int SourceId { get; set; }

    public long TotalRecords { get; set; }

    // Completeness

    public long RecordsWithPrice { get; set; }

    public long RecordsWithoutPrice { get; set; }

    public long RecordsWithYear { get; set; }

    public long RecordsWithoutYear { get; set; }

    public long RecordsWithMileage { get; set; }

    public long RecordsWithoutMileage { get; set; }

    public long RecordsWithEngineCapacity { get; set; }

    public long RecordsWithoutEngineCapacity { get; set; }

    public long RecordsWithMake { get; set; }

    public long RecordsWithoutMake { get; set; }

    public long RecordsWithModel { get; set; }

    public long RecordsWithoutModel { get; set; }

    public long RecordsWithVariant { get; set; }

    public long RecordsWithoutVariant { get; set; }

    public long RecordsWithCity { get; set; }

    public long RecordsWithoutCity { get; set; }

    public long RecordsWithProvince { get; set; }

    public long RecordsWithoutProvince { get; set; }

    // Validity

    public long InvalidPriceRecords { get; set; }

    public long SuspiciousLowPriceRecords { get; set; }

    public long SuspiciousHighPriceRecords { get; set; }

    public long InvalidMileageRecords { get; set; }

    public long SuspiciousHighMileageRecords { get; set; }

    public long InvalidYearRecords { get; set; }

    public long InvalidEngineCapacityRecords { get; set; }

    public long SuspiciousEngineCapacityRecords { get; set; }

    // Normalization

    public int DistinctMakes { get; set; }

    public int DistinctModels { get; set; }

    public int DistinctVariants { get; set; }

    public int DistinctVehicleNames { get; set; }

    public int DistinctLocations { get; set; }

    // Valuation readiness

    public long BasicValuationReadyRecords { get; set; }

    public long DetailedValuationReadyRecords { get; set; }

    // Price statistics

    public decimal? AverageAskingPrice { get; set; }

    public decimal? MinimumAskingPrice { get; set; }

    public decimal? MaximumAskingPrice { get; set; }

    public int? MinimumYear { get; set; }

    public int? MaximumYear { get; set; }

    public int? MinimumMileage { get; set; }

    public int? MaximumMileage { get; set; }

    public int? MinimumEngineCapacity { get; set; }

    public int? MaximumEngineCapacity { get; set; }

    // Coverage percentages

    public decimal PriceCoveragePercentage { get; set; }

    public decimal YearCoveragePercentage { get; set; }

    public decimal MileageCoveragePercentage { get; set; }

    public decimal MakeCoveragePercentage { get; set; }

    public decimal ModelCoveragePercentage { get; set; }

    public decimal LocationCoveragePercentage { get; set; }

    public decimal BasicValuationReadinessPercentage { get; set; }

    public decimal DetailedValuationReadinessPercentage { get; set; }
}