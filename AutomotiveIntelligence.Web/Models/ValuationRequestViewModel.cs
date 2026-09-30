using System.ComponentModel.DataAnnotations;

namespace AutomotiveIntelligence.Web.Models;

public class ValuationRequestViewModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Please select a make.")]
    public int MakeId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a model.")]
    public int ModelId { get; set; }

    public int? VariantId { get; set; }

    public int? ManufacturingYear { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Mileage cannot be negative.")]
    public int? Mileage { get; set; }

    // These two fields are used by the Web UI
    // to maintain the Country → Province → City selection.
    // Only CityId is sent to the valuation API.
    public int? CountryId { get; set; }

    public int? ProvinceId { get; set; }

    public int? CityId { get; set; }

    public int? ConditionId { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Asking price cannot be negative.")]
    public decimal? AskingPrice { get; set; }

    public bool? HasAccidentHistory { get; set; }

    [Range(1, 20, ErrorMessage = "Owner count must be between 1 and 20.")]
    public int? OwnerCount { get; set; }
}