namespace RefactoringLab;

public interface IOrderRepository
{
    void Save(int orderId, DateTime processedAt);
}

public interface IEmailSender
{
    void Send(string to, string body);
}


/// Depends on abstractions (DIP). The concrete repository and e-mail sender are supplied
/// from outside, so they can be swapped or replaced with fakes in tests.

public class OrderProcessor
{
    private readonly IOrderRepository _repository;
    private readonly IEmailSender _emailSender;
    private readonly TimeProvider _clock;

    public OrderProcessor(IOrderRepository repository, IEmailSender emailSender, TimeProvider? clock = null)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _clock = clock ?? TimeProvider.System;
    }

    public void Process(int orderId, string customerEmail)
    {
        var now = _clock.GetLocalNow().LocalDateTime;

        _repository.Save(orderId, now);
        _emailSender.Send(customerEmail, $"Order {orderId} confirmed at {now}");
    }
}

public class SqlOrderRepository : IOrderRepository
{
    public void Save(int orderId, DateTime processedAt) =>
        Console.WriteLine($"[SQL] save order {orderId} @ {processedAt:O}");
}

public class SmtpEmailSender : IEmailSender
{
    public void Send(string to, string body) =>
        Console.WriteLine($"[SMTP] to={to} body={body}");
}
