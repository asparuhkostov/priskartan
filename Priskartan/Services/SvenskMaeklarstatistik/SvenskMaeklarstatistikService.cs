using HtmlAgilityPack;

namespace Priskartan.Services.SvenskMaeklarstatistik;

public class SvenskMaeklarstatistikService : ISvenskMaeklarstatistikService
{
    private static readonly string BaseUrl = "https://www.maklarstatistik.se";
    private static readonly string KingdomAreaUrl = $"{BaseUrl}/omrade/riket/";
    private static readonly List<string> swedishRegions = new List<string>()
    {
        "Blekinge län",
        "Dalarnas län",
        "Gotlans länd",
        "Gävleborgs län",
        "Hallands län",
        "Jämtlands län",
        "Jönköpings län",
        "Kalmar län",
        "Kronobergs län",
        "Norrbottens län",
        "Skåne län",
        "Stockholms län",
        "Södermanlands län",
        "Uppsala län",
        "Värmlands län",
        "Västerbottens län",
        "Västernorrlands län",
        "Västmanlands län",
        "Västra Götalands län",
        "Örebro län",
        "Östergötlands län"
    };

    public Dictionary<string, int> LoadRealEstatePriceData()
    {
        var finalReport = new Dictionary<string, int>();

        var web = new HtmlWeb();
        var doc = web.Load(KingdomAreaUrl);
        var tableData = doc.DocumentNode.SelectNodes("//table[contains(@class, 'width-100') and contains(@class, 'sortable-table')]/tbody/tr/td[4]\r\n");
        var usefulTableData = tableData.Skip(4).Take(20);
        
        for(var  i = 0; i < usefulTableData.Count(); i++)
        {
            var priceString = usefulTableData.ElementAt(i).InnerText.Replace(" ", "");
            finalReport.Add(swedishRegions[i], int.Parse(priceString));
        }

        return finalReport;
    }
}
