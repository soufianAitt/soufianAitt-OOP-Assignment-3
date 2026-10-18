namespace RefactoringLab;


/// NEW carrier (R5). Added as a new file only — ShippingCostCalculator and the other
/// carriers were not touched.

public sealed class BostaCarrier : IShippingCarrier
{
    public string Name => "Bosta";
    public decimal Calculate(decimal weightKg) => weightKg * 10m;
}
