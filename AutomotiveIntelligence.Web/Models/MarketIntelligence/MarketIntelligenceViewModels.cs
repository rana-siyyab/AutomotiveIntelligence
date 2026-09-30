namespace AutomotiveIntelligence.Web.Models.MarketIntelligence;

public class MarketIntelligenceOverviewViewModel
{
    public int TotalMarketRecords { get; set; }

    public int TotalMakes { get; set; }

    public int TotalModels { get; set; }

    public int TotalCities { get; set; }

    public decimal AverageMarketPrice { get; set; }

    public decimal MinimumMarketPrice { get; set; }

    public decimal MaximumMarketPrice { get; set; }

    public decimal LatestAverageMarketPrice { get; set; }

    public DateTime? LatestObservationDate { get; set; }
}


public class MarketPriceByMakeViewModel
{
    public int MakeId { get; set; }

    public string MakeName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}


public class MarketPriceByModelViewModel
{
    public int ModelId { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public int MakeId { get; set; }

    public string MakeName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}


public class MarketPriceByCityViewModel
{
    public int CityId { get; set; }

    public string CityName { get; set; } = string.Empty;

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}


public class MarketPriceByYearViewModel
{
    public int Year { get; set; }

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}


public class MarketPriceTrendViewModel
{
    public DateTime Date { get; set; }

    public int RecordCount { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal MinimumPrice { get; set; }

    public decimal MaximumPrice { get; set; }
}


public class MarketIntelligenceViewModel
{
    public MarketIntelligenceOverviewViewModel Overview { get; set; } = new();

    public IReadOnlyList<MarketPriceByMakeViewModel> ByMake { get; set; } = [];

    public IReadOnlyList<MarketPriceByModelViewModel> ByModel { get; set; } = [];

    public IReadOnlyList<MarketPriceByCityViewModel> ByCity { get; set; } = [];

    public IReadOnlyList<MarketPriceByYearViewModel> ByYear { get; set; } = [];

    public IReadOnlyList<MarketPriceTrendViewModel> PriceTrends { get; set; } = [];
}