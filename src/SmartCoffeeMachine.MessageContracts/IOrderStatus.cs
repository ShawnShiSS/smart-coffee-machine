using System;

namespace SmartCoffeeMachine.MessageContracts
{
    /// <summary>
    ///     Message contract for the status of an order.
    /// </summary>
    public interface IOrderStatus
    {
        Guid OrderId { get; }
        string State { get; }
    }
}
