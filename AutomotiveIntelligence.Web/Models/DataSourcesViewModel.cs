using AutomotiveIntelligence.Web.Models.Admin;

namespace AutomotiveIntelligence.Web.Models;

public class DataSourcesViewModel
{
    public IReadOnlyList<DataSourceViewModel> Sources { get; set; } = [];

    public CreateDataSourceViewModel Create { get; set; } = new();
}