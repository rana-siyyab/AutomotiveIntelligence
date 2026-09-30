using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AutomotiveIntelligence.Web.Models.Admin;

public class MarketDataImportViewModel
{
    [Required]
    [Display(Name = "Data Source")]
    public int SourceId { get; set; }

    public IReadOnlyList<DataSourceViewModel> DataSources { get; set; }
        = [];

    [Required]
    [Display(Name = "CSV File")]
    public IFormFile? File { get; set; }

    public MarketDataImportResultViewModel? Result { get; set; }
}

public class MarketDataImportResultViewModel
{
    public bool Success { get; set; }

    public long ImportId { get; set; }

    public int TotalRows { get; set; }

    public int SuccessfulRows { get; set; }

    public int FailedRows { get; set; }

    public int ImportedVehicles { get; set; }

    public int ImportedListings { get; set; }

    public int ImportedMarketPrices { get; set; }

    public List<MarketDataImportErrorViewModel> Errors { get; set; } = [];
    public int DuplicateRows { get; set; }
}

public class MarketDataImportErrorViewModel
{
    public int RowNumber { get; set; }

    public string ErrorType { get; set; } = string.Empty;

    public string Error { get; set; } = string.Empty;
}