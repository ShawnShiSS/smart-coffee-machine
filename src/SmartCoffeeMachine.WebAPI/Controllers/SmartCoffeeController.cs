using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCoffeeMachine.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace SmartCoffeeMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartCoffeeController : ControllerBase
    {
        
        private readonly ILogger<SmartCoffeeController> _logger;

        public SmartCoffeeController(ILogger<SmartCoffeeController> logger)
        {
            _logger = logger;
        }

        // POST: api/SmartCoffee
        /// <summary>
        ///     Request to make a new coffee 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCoffeeModel createCoffeeModel)
        {
            Guid orderId = Guid.NewGuid();

            // TODO : decouple long-running task using async message queue.

            return Ok(orderId);
        }

    }
}
