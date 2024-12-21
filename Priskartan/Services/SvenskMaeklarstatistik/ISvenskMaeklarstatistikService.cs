namespace Priskartan.Services.SvenskMaeklarstatistik;

public interface ISvenskMaeklarstatistikService
{
    Dictionary<string, int> LoadRealEstatePriceData();
}
