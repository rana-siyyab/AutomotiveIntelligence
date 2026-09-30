using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class DataImportRepository : IDataImportRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public DataImportRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DataImport>>
        GetRecentAsync(
            int pageNumber,
            int pageSize)
    {
        return await _context.DataImports
            .AsNoTracking()
            .OrderByDescending(x => x.StartedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<DataImport?> GetByIdAsync(
        long importId)
    {
        return await _context.DataImports
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.ImportId == importId);
    }
}