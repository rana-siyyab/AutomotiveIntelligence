namespace AutomotiveIntelligence.Web.Models.Dashboard;

public class DashboardViewModel
{
    public MarketOverviewViewModel MarketOverview { get; set; }
        = new();

    public IReadOnlyList<PriceTrendViewModel> PriceTrends { get; set; }
        = [];

    public IReadOnlyList<PopularModelViewModel> PopularModels { get; set; }
        = [];

    public IReadOnlyList<RecentValuationViewModel> RecentValuations { get; set; }
        = [];
}