namespace AutomotiveIntelligence.Application.DTOs;

public class VehicleConditionDto
{
    public int ConditionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public string? Description { get; set; }
}