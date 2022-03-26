namespace SmartCoffeeMachine.WebAPI.Models
{
    /// <summary>
    ///     API model to create a new coffee
    /// </summary>
    public class CreateCoffeeModel
    {
        /// <summary>
        ///     Type of coffee. I.e., French Press.
        /// </summary>
        public string Type { get; set; }

    }
}
