namespace Priskartan.Services.Eon;

public interface IEonService
{
    Task<Dictionary<string, double>> GetSpotPricePerRegion();
}
