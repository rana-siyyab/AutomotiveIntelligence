using AutomotiveIntelligence.Web.Models.Dashboard;

namespace AutomotiveIntelligence.Web.Models;

public class AdminDashboardViewModel
{
    public MarketOverviewViewModel MarketOverview { get; set; } = new();

    public DataQualityViewModel DataQuality { get; set; } = new();

    public IReadOnlyList<PriceTrendViewModel> PriceTrends { get; set; } = [];

    public IReadOnlyList<PopularModelViewModel> PopularModels { get; set; } = [];

    public IReadOnlyList<PamaYearlySummaryViewModel> PamaYearlySummary { get; set; } = [];

    public IReadOnlyList<OicaYearlySummaryViewModel> OicaYearlySummary { get; set; } = [];
}