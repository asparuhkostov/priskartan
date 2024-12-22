using HtmlAgilityPack;
using System.Collections.Generic;

namespace Priskartan.Services.SvenskMaeklarstatistik;

public class SvenskMaeklarstatistikService : ISvenskMaeklarstatistikService
{
    private static readonly string BaseUrl = "https://www.maklarstatistik.se";
    private static readonly string KingdomAreaUrl = $"{BaseUrl}/omrade/riket/";
    private static readonly List<string> swedishRegions = new List<string>()
    {
        "Blekinge",
        "Dalarna",
        "Gotland",
        "Gävleborg",
        "Halland",
        "Jämtland",
        "Jönköping",
        "Kalmar",
        "Kronoberg",
        "Norrbotten",
        "Skåne",
        "Stockholm",
        "Södermanland",
        "Uppsala",
        "Värmland",
        "Västerbotten",
        "Västernorrland",
        "Västmanland",
        "Västra Götaland",
        "Örebro",
        "Östergötland"
    };

    public Dictionary<string, int> LoadRealEstatePriceData()
    {
        var finalReport = new Dictionary<string, int>();

        var web = new HtmlWeb();
        var doc = web.Load(KingdomAreaUrl);
        var tableData = doc.DocumentNode.SelectNodes("//table[contains(@class, 'width-100') and contains(@class, 'sortable-table')]/tbody/tr/td[4]\r\n");
        var usefulTableData = tableData.Skip(4).Take(21);
        
        for(var  i = 0; i < usefulTableData.Count(); i++)
        {
            var priceString = usefulTableData.ElementAt(i).InnerText.Replace(" ", "");
            finalReport.Add(swedishRegions[i], int.Parse(priceString));
        }

        return finalReport;
    }
}
