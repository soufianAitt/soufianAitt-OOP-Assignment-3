namespace RefactoringLab;


/// A delivery channel (e-mail, SMS, push, ...). It only knows HOW to deliver.
/// "Urgent" and "scheduled" are not channels — they are options handled by <see cref="Notifier"/>.

public interface INotificationChannel
{
    string Name { get; }
    void Deliver(string to, string message, DateTime? sendAt);
}


public static class NotificationFormat
{
    public static string Line(string channelName, string to, string message, DateTime? sendAt)
    {
        var tag = sendAt is { } at ? $"{channelName} scheduled {at:g}" : channelName;
        return $"[{tag}] {to}: {message}";
    }
}

public sealed class EmailChannel : INotificationChannel
{
    public string Name => "email";

    public void Deliver(string to, string message, DateTime? sendAt) =>
        Console.WriteLine(NotificationFormat.Line(Name, to, message, sendAt));
}

public sealed class SmsChannel : INotificationChannel
{
    public string Name => "sms";

    public void Deliver(string to, string message, DateTime? sendAt) =>
        Console.WriteLine(NotificationFormat.Line(Name, to, message, sendAt));
}


/// One class for every combination. A channel is plugged in, and urgent / scheduled are
/// plain options — so email, SMS (or any new channel) can be normal, urgent, scheduled,
/// or urgent + scheduled without a class per combination.

public sealed class Notifier
{
    private const string UrgentPrefix = "[URGENT] ";

    private readonly INotificationChannel _channel;
    private readonly bool _urgent;
    private readonly DateTime? _sendAt;

    public Notifier(INotificationChannel channel, bool urgent = false, DateTime? sendAt = null)
    {
        _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        _urgent = urgent;
        _sendAt = sendAt;
    }

    public void Send(string to, string message)
    {
        var body = _urgent ? UrgentPrefix + message : message;
        _channel.Deliver(to, body, _sendAt);
    }
}
