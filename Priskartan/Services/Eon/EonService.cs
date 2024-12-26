namespace Priskartan.Services.Eon;

using Priskartan.Models;
using Priskartan.Services.Eon.Client;
using Priskartan.Shared.Constants;

public class EonService : IEonService
{
    private readonly EonClient _client = new EonClient();
    // TO-DO: split the sectors based on municipality instead, as some regions are split between sectors.
    private readonly Dictionary<string, List<string>> _regionsPerElectricalSector = new Dictionary<string, List<string>>()
    {
        { "SE1", new List<string> { SwedishRegions.NORRBOTTEN } },
        { "SE2", new List<string> { SwedishRegions.JAMTLAND, SwedishRegions.VASTERNORRLAND, SwedishRegions.VASTERBOTTEN, SwedishRegions.GAVLEBORG } },
        { "SE3", new List<string> { SwedishRegions.VARMLAND, SwedishRegions.VASTMANLAND, SwedishRegions.OREBRO, SwedishRegions.UPPSALA, SwedishRegions.STOCKHOLM, SwedishRegions.SODERMANLAND, SwedishRegions.OSTERGOTLAND, SwedishRegions.GOTLAND, SwedishRegions.DALARNA, SwedishRegions.JONKOPING, SwedishRegions.VASTRA_GOTALAND } },
        { "SE4", new List<string> { SwedishRegions.SKANE, SwedishRegions.BLEKINGE, SwedishRegions.KRONOBERG, SwedishRegions.KALMAR, SwedishRegions.HALLAND } }
    };

    private async Task<EonSpotPrice> GetPricesAsync()
    {
        return await _client.GetSectorSpotPricesAsync();
    }

    public async Task<Dictionary<string, double>> GetSpotPricePerRegionAsync()
    {
        var finalData = new Dictionary<string, double>();
        var priceData = await GetPricesAsync();

        //There are 4 electrical-network sectors in Sweden,
        // so we need to map each of the regions to a sector.
        foreach (var (sector, regions) in _regionsPerElectricalSector)
        {
            foreach(var region in regions)
            {
                var sectorPrice = priceData.MonthlyAveragePrice.First(ap => ap.PriceArea == sector);
                finalData.Add(region, sectorPrice.Price);
            }
        }

        return finalData;
    }
}
