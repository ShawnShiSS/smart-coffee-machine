using System;

namespace SmartCoffeeMachine.MessageContracts
{
    /// <summary>
    ///     Message contract to check the status of an order.
    /// </summary>
    public interface ICheckOrder
    {
        Guid OrderId { get; }
    }
}
