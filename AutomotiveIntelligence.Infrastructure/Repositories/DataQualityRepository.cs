using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class DataQualityRepository : IDataQualityRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public DataQualityRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetTotalImportsAsync()
    {
        return await _context.DataImports
            .CountAsync();
    }

    public async Task<int> GetTotalRecordsProcessedAsync()
    {
        return await _context.DataImports
            .SumAsync(x => x.RecordsProcessed);
    }

    public async Task<int> GetTotalRecordsInsertedAsync()
    {
        return await _context.DataImports
            .SumAsync(x => x.RecordsInserted);
    }

    public async Task<int> GetTotalRecordsRejectedAsync()
    {
        return await _context.DataImports
            .SumAsync(x => x.RecordsRejected);
    }

    public async Task<int> GetTotalRecordsDuplicatedAsync()
    {
        return await _context.DataImports
            .SumAsync(x => x.RecordsDuplicated);
    }

    public async Task<int> GetSuccessfulImportsAsync()
    {
        return await _context.DataImports
            .CountAsync(x =>
                x.ImportStatus == "Completed");
    }

    public async Task<int> GetImportsWithErrorsAsync()
    {
        return await _context.DataImports
            .CountAsync(x =>
                x.RecordsRejected > 0);
    }

    public async Task<DateTime?> GetLastImportDateAsync()
    {
        return await _context.DataImports
            .OrderByDescending(x => x.StartedDate)
            .Select(x => (DateTime?)x.StartedDate)
            .FirstOrDefaultAsync();
    }
}