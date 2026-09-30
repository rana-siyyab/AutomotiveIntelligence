using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using AutomotiveIntelligence.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class DataSourceRepository : IDataSourceRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public DataSourceRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DataSource>> GetActiveAsync()
    {
        return await _context.DataSources
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<DataSource?> GetByIdAsync(int sourceId)
    {
        return await _context.DataSources
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.SourceId == sourceId &&
                     x.IsActive);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.DataSources
            .AnyAsync(x => x.Name == name);
    }

    public async Task<DataSource> CreateAsync(DataSource source)
    {
        _context.DataSources.Add(source);

        await _context.SaveChangesAsync();

        return source;
    }
    public async Task<IReadOnlyList<DataSource>> GetAllAsync()
    {
        return await _context.DataSources
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
    public async Task<bool> SetActiveStatusAsync(
    int sourceId,
    bool isActive)
    {
        var source = await _context.DataSources
            .FirstOrDefaultAsync(x => x.SourceId == sourceId);

        if (source == null)
        {
            return false;
        }

        source.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }
}