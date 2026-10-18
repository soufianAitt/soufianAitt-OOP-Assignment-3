namespace RefactoringLab;

public sealed class PushChannel : INotificationChannel
{
    public string Name => "push";

    public void Deliver(string to, string message, DateTime? sendAt) =>
        Console.WriteLine(NotificationFormat.Line(Name, to, message, sendAt));
}
