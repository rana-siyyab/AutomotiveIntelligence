using AutomotiveIntelligence.Domain.Entities;

namespace AutomotiveIntelligence.Application.Interfaces;

public interface IVehicleConditionRepository
{
    Task<VehicleCondition?> GetByIdAsync(int conditionId);
}