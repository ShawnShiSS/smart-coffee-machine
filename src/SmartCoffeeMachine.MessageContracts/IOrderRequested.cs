using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCoffeeMachine.MessageContracts
{
    public interface IOrderRequested
    {
        /// <summary>
        /// Order Id.
        /// </summary>
        Guid OrderId { get; }

        /// <summary>
        /// Coffee type.
        /// </summary>
        string Type { get; }
    }

}
