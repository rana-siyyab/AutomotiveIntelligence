namespace AutomotiveIntelligence.Domain.Entities;

public class VehicleCondition
{
    public int ConditionId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Score { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}