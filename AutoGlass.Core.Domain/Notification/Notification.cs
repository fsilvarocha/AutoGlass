using AutoGlass.Core.Domain.Interfaces;

namespace AutoGlass.Core.Domain.Notification;

public sealed class Notification : INotification
{
    public Notification(string message, string propertyName)
    {
        Message = message;
        PropertyName = propertyName;
    }

    public string Message { get; private set; }
    public string PropertyName { get; private set; }
}
