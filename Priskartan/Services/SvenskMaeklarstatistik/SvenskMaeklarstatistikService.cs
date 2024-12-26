using HtmlAgilityPack;
using Priskartan.Shared.Constants;

namespace Priskartan.Services.SvenskMaeklarstatistik;

public class SvenskMaeklarstatistikService : ISvenskMaeklarstatistikService
{
    private static readonly string BaseUrl = "https://www.maklarstatistik.se";
    private static readonly string KingdomAreaUrl = $"{BaseUrl}/omrade/riket/";
    private static readonly List<string> swedishRegions = new List<string>()
    {
        SwedishRegions.BLEKINGE,
        SwedishRegions.DALARNA,
        SwedishRegions.GOTLAND,
        SwedishRegions.GAVLEBORG,
        SwedishRegions.HALLAND,
        SwedishRegions.JAMTLAND,
        SwedishRegions.JONKOPING,
        SwedishRegions.KALMAR,
        SwedishRegions.KRONOBERG,
        SwedishRegions.NORRBOTTEN,
        SwedishRegions.SKANE,
        SwedishRegions.STOCKHOLM,
        SwedishRegions.SODERMANLAND,
        SwedishRegions.UPPSALA,
        SwedishRegions.VARMLAND,
        SwedishRegions.VASTERBOTTEN,
        SwedishRegions.VASTERNORRLAND,
        SwedishRegions.VASTMANLAND,
        SwedishRegions.VASTRA_GOTALAND,
        SwedishRegions.OREBRO,
        SwedishRegions.OSTERGOTLAND
    };

    public Dictionary<string, int> GetRealEstatePriceData()
    {
        var finalReport = new Dictionary<string, int>();

        var web = new HtmlWeb();
        var doc = web.Load(KingdomAreaUrl);
        var tableData = doc.DocumentNode.SelectNodes("//table[contains(@class, 'width-100') and contains(@class, 'sortable-table')]/tbody/tr/td[4]\r\n");
        
        // The website in question has a few entries that aggregate numbers for larger sectors of Sweden.
        // We want to skip those and get only the regional average prices.
        var usefulTableData = tableData.Skip(4).Take(21);
        
        for(var  i = 0; i < usefulTableData.Count(); i++)
        {
            var priceString = usefulTableData.ElementAt(i).InnerText.Replace(" ", "");
            finalReport.Add(swedishRegions[i], int.Parse(priceString));
        }

        return finalReport;
    }
}
