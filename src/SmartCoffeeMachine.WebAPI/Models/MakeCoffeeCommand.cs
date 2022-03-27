using System;

namespace SmartCoffeeMachine.WebAPI.Models
{
    /// <summary>
    ///     API command to create a new coffee
    /// </summary>
    public class MakeCoffeeCommand
    {
        /// <summary>
        ///     Type of coffee. I.e., French Press.
        /// </summary>
        public string CoffeeType { get; set; }

    }

    /// <summary>
    ///     API response for a create coffee request.
    /// </summary>
    public class MakeCoffeeResponse
    { 
        public Guid OrderId { get; set; }
    }
}
