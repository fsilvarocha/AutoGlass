using AutoGlass.Core.Domain.Notification;

namespace AutoGlass.Core.Domain.Validation;


public partial class ContractValidations<T>
{
    public ContractValidations<T> ValidManufactureIsOk(DateTime Manufacturing, DateTime Validate, string message, string propertyName)
    {
        if (Manufacturing >= Validate)
            AddNotification(new Notification.Notification(message, propertyName));

        return this;

    }

}
