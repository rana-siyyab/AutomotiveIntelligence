using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class VehicleConditionRepository : IVehicleConditionRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public VehicleConditionRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<VehicleCondition?> GetByIdAsync(int conditionId)
    {
        return await _context.VehicleConditions
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.ConditionId == conditionId &&
                x.IsActive);
    }
}