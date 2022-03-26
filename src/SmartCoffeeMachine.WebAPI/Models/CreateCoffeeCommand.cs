using System;

namespace SmartCoffeeMachine.WebAPI.Models
{
    /// <summary>
    ///     API command to create a new coffee
    /// </summary>
    public class CreateCoffeeCommand
    {
        /// <summary>
        ///     Type of coffee. I.e., French Press.
        /// </summary>
        public string Type { get; set; }

    }

    /// <summary>
    ///     API response for a create coffee request.
    /// </summary>
    public class CreateCoffeeResponse
    { 
        public Guid OrderId { get; set; }
    }
}
