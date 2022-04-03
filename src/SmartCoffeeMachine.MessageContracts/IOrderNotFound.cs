using System;

namespace SmartCoffeeMachine.MessageContracts
{
    /// <summary>
    ///     Message contract for an order not found message.
    /// </summary>
    public interface IOrderNotFound
    {
        Guid OrderId { get; }
    }
}
