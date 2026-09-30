namespace AutomotiveIntelligence.Web.Models.Dashboard;

public class PopularModelViewModel
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }
}