namespace AutomotiveIntelligence.Application.DTOs;

public class ValuationRequestDto
{
    public int MakeId { get; set; }

    public int ModelId { get; set; }

    public int? VariantId { get; set; }

    public int? ManufacturingYear { get; set; }

    public int? Mileage { get; set; }

    public int? CityId { get; set; }

    public int? ConditionId { get; set; }

    public decimal? AskingPrice { get; set; }

    public bool? HasAccidentHistory { get; set; }

    public int? OwnerCount { get; set; }
}