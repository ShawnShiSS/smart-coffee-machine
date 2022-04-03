using System;

namespace SmartCoffeeMachine.MessageContracts
{
    public interface IOrderCompleted
    {
        Guid OrderId { get; }

        DateTime Timestamp { get; }
    }
}
