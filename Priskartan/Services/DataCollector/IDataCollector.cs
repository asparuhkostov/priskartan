namespace Priskartan.Services.DataCollector;

public interface IDataCollector
{
    public Task<bool> CollectPricingData();
}
