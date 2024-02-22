namespace DesignPattern;

public interface IShippingStrategyFactory
{
    IShippingStrategy? GetShippingStrategy(Order order);
}

public class ShippingStrategyFactory(IServiceProvider serviceProvider) : IShippingStrategyFactory
{
    public IShippingStrategy? GetShippingStrategy(Order order)
    {
        var shippingStrategy = serviceProvider.GetKeyedService<IShippingStrategy>(order.ShippingMethod);
        ArgumentNullException.ThrowIfNull(shippingStrategy, "strategy is null");

        return shippingStrategy;
    }
}

public class FedexShippingStrategy : IShippingStrategy
{
    public string CalculateShippingCost(Order order)
    {
        return "FedexShippingStrategy";
    }
}

public class DHLShippingStrategy : IShippingStrategy
{
    public string CalculateShippingCost(Order order)
    {
        return "DHLShippingStrategy";
    }
}

public class UPSShippingStrategy : IShippingStrategy
{
    public string CalculateShippingCost(Order order)
    {
        return "UPSShippingStrategy";
    }
}

public class ArasShippingStrategy : IShippingStrategy
{
    public string CalculateShippingCost(Order order)
    {
        return "ArasShippingStrategy";
    }
}

public class Order
{
    public string ShippingMethod { get; set; }
}

public interface IShippingStrategy
{
    string CalculateShippingCost(Order order);
}