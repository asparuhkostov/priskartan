using System.Text.Json;
using Priskartan.Models;

namespace Priskartan.Services.Eon.Client;

public class EonClient
{
    private const string EonBaseUrl = "https://eonspotpricesapirun.azurewebsites.net/api";
    private const string SpotPricesUrl = $"{EonBaseUrl}/spotprices";

    private HttpClient _httpClient = new HttpClient();

    public async Task<EonSpotPrice> GetRegionalSpotPricesAsync()
    {
        var response = await _httpClient.GetAsync(SpotPricesUrl);
        var ApiResponse = await response.Content.ReadAsStringAsync();
        if(ApiResponse is null)
        {
            throw new Exception("Could not fetch the electricity spot prices.");
        }

        EonSpotPrice? price = JsonSerializer.Deserialize<EonSpotPrice>(ApiResponse);

        if (price is not null)
        {
            return price;
        }
        else
        {
            throw new Exception("Could not process the electricity spot prices.");
        }
    }
}
