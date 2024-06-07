using AutoGlass.Core.Domain.Interfaces;

namespace AutoGlass.Core.Domain.Validation;

public partial class ContractValidations<T> where T : IInterface
{
    private List<Notification.Notification> _notifications;

    public ContractValidations() => _notifications = new();
    public IReadOnlyCollection<Notification.Notification> Notifications => _notifications;
    public void AddNotification(Notification.Notification notification) => _notifications.Add(notification);
    public bool IsValid() => _notifications.Count == 0;
}
