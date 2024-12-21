namespace Priskartan.Services.Eon;

using Priskartan.Models;

public interface IEonService
{
    Task<EonSpotPrice> GetPricesAsync();
}
