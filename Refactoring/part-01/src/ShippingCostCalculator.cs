namespace RefactoringLab;


/// Open for extension, closed for modification: it knows nothing about specific carriers.
/// It just looks the carrier up by name among the <see cref="IShippingCarrier"/>s it was given.

public class ShippingCostCalculator
{
    private readonly Dictionary<string, IShippingCarrier> _carriers;

    public ShippingCostCalculator(IEnumerable<IShippingCarrier> carriers)
    {
        ArgumentNullException.ThrowIfNull(carriers);
        _carriers = carriers.ToDictionary(c => c.Name);
    }

    public decimal Calculate(string carrier, decimal weightKg)
    {
        if (carrier is null || !_carriers.TryGetValue(carrier, out var found))
            throw new ArgumentException($"Unknown carrier: {carrier}");

        return found.Calculate(weightKg);
    }
}
