namespace Priskartan.Services.Eon;

using Priskartan.Models;
using Priskartan.Services.Eon.Client;

public class EonService : IEonService
{
    private readonly EonClient _client = new EonClient();

    public async Task<EonSpotPrice> GetPricesAsync()
    {
        return await _client.GetRegionalSpotPricesAsync();
    }
}
