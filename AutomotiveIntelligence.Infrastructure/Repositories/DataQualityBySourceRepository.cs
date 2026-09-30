using AutomotiveIntelligence.Application.DTOs;
using AutomotiveIntelligence.Application.Interfaces;
using AutomotiveIntelligence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AutomotiveIntelligence.Infrastructure.Repositories;

public class DataQualityBySourceRepository
    : IDataQualityBySourceRepository
{
    private readonly AutomotiveIntelligenceDbContext _context;

    public DataQualityBySourceRepository(
        AutomotiveIntelligenceDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DataQualityBySourceDto>>
        GetBySourceAsync()
    {
        var results = await (
            from import in _context.DataImports.AsNoTracking()
            join source in _context.DataSources.AsNoTracking()
                on import.SourceId equals source.SourceId
            group import by new
            {
                source.SourceId,
                source.Name,
                source.SourceType
            }
            into grouped
            select new DataQualityBySourceDto
            {
                SourceId = grouped.Key.SourceId,
                SourceName = grouped.Key.Name,
                SourceType = grouped.Key.SourceType,

                TotalImports = grouped.Count(),

                RecordsProcessed =
                    grouped.Sum(x => x.RecordsProcessed),

                RecordsInserted =
                    grouped.Sum(x => x.RecordsInserted),

                RecordsRejected =
                    grouped.Sum(x => x.RecordsRejected),

                RecordsDuplicated =
                    grouped.Sum(x => x.RecordsDuplicated)
            })
            .OrderBy(x => x.SourceName)
            .ToListAsync();

        foreach (var result in results)
        {
            if (result.RecordsProcessed > 0)
            {
                result.RejectionRate =
                    Math.Round(
                        result.RecordsRejected * 100m /
                        result.RecordsProcessed,
                        2);

                result.DuplicateRate =
                    Math.Round(
                        result.RecordsDuplicated * 100m /
                        result.RecordsProcessed,
                        2);
            }
        }

        return results;
    }
}