using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCoffeeMachine.WebAPI.Models;
using System.Threading.Tasks;

namespace SmartCoffeeMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleCoffeeController : ControllerBase
    {
        
        private readonly ILogger<SimpleCoffeeController> _logger;

        public SimpleCoffeeController(ILogger<SimpleCoffeeController> logger)
        {
            _logger = logger;
        }

        // POST: api/SimpleCoffee
        /// <summary>
        ///     Request to make a new coffee 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoffeeCommand createCoffeeModel)
        {
            await Task.Delay(3000);

            return StatusCode(StatusCodes.Status408RequestTimeout);
        }

    }
}
