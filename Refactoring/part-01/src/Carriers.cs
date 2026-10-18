namespace RefactoringLab;


/// A shipping carrier. To support a new carrier, add a new class that implements this
/// interface and register it where the calculator is created — no existing class changes.

public interface IShippingCarrier
{
    string Name { get; }
    decimal Calculate(decimal weightKg);
}

public sealed class AramexCarrier : IShippingCarrier
{
    public string Name => "Aramex";
    public decimal Calculate(decimal weightKg) => weightKg * 12m;
}

public sealed class FedExCarrier : IShippingCarrier
{
    public string Name => "FedEx";
    public decimal Calculate(decimal weightKg) => weightKg * 15m;
}

public sealed class DhlCarrier : IShippingCarrier
{
    public string Name => "DHL";
    public decimal Calculate(decimal weightKg) => weightKg * 18m;
}
