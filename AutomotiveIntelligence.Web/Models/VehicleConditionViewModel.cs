namespace AutomotiveIntelligence.Web.Models;

public class VehicleConditionViewModel
{
    public int ConditionId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Score { get; set; }

    public string? Description { get; set; }
}