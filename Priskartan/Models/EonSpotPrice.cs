namespace Priskartan.Models;

public class EonMonthlyAveragePrice
{
    public string PriceArea { get; set; } = string.Empty;
    public double Price { get; set; }
}

public class EonSpotPrice
{
    string Timestamp { get; set; } = string.Empty;
    public string Month { get; set; } = string.Empty;
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public List<EonMonthlyAveragePrice> MonthlyAveragePrice { get; set; } = new();
}
