namespace Priskartan.Services.DataCollector;

using Priskartan.Services.Eon;
using Priskartan.Services.SvenskMaeklarstatistik;

public class DataCollector : IDataCollector
{
    private IEonService _eonService { get; set; }
    private ISvenskMaeklarstatistikService _svenskMaeklarstatistikService { get; set; }

    public DataCollector(IEonService EonService, ISvenskMaeklarstatistikService SvenskMaeklarStatistikService)
    {
        _eonService = EonService;
        _svenskMaeklarstatistikService = SvenskMaeklarStatistikService;
    }

    public async Task<bool> CollectPricingData()
    {
        var electricityPricingData = await _eonService.GetSpotPricePerRegionAsync();
        var realEstatePricingData = _svenskMaeklarstatistikService.GetRealEstatePriceData();

        // TO-DO: Add a db and use models to store fetched data (or use a cache).
        // The application should not have to make these calls on every page visit.

        return true;
    }
}
