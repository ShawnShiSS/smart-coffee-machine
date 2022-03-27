using System;

namespace SmartCoffeeMachine.MessageContracts
{
    public interface IOrderAccepted
    {
        /// <summary>
        /// Order Id.
        /// </summary>
        Guid OrderId { get; }

        /// <summary>
        /// Coffee type.
        /// </summary>
        string CoffeeType { get; }
    }
}
