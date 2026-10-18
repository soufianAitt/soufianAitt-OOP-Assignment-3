using RefactoringLab;

// Composition root: the only place that knows the concrete types.
var shipping = new ShippingCostCalculator(
[
    new AramexCarrier(),
    new FedExCarrier(),
    new DhlCarrier(),
    new BostaCarrier()   // new carrier (R5)
]);
Console.WriteLine($"Aramex 2kg → {shipping.Calculate("Aramex", 2)}");
Console.WriteLine($"FedEx 2kg  → {shipping.Calculate("FedEx", 2)}");
Console.WriteLine($"Bosta 2kg  → {shipping.Calculate("Bosta", 2)}");
Console.WriteLine();

var processor = new OrderProcessor(new SqlOrderRepository(), new SmtpEmailSender());
processor.Process(1001, "customer@example.com");
Console.WriteLine();

var sendAt = DateTime.Today.AddHours(18);

// Same output as before:
new Notifier(new EmailChannel(), urgent: true, sendAt: sendAt)
    .Send("customer@example.com", "Your order ships tomorrow");
new Notifier(new SmsChannel(), urgent: true)
    .Send("+201000000000", "OTP 4821");

// New channel (R6), plus every combination without a class per combination:
new Notifier(new PushChannel(), urgent: true, sendAt: sendAt)
    .Send("device-42", "Your driver is nearby");
new Notifier(new PushChannel())
    .Send("device-42", "Plain push notification");
new Notifier(new EmailChannel())
    .Send("customer@example.com", "Plain email");
new Notifier(new SmsChannel(), sendAt: sendAt)
    .Send("+201000000000", "Scheduled (not urgent) SMS");
