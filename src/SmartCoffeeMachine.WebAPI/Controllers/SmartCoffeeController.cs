using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCoffeeMachine.MessageContracts;
using SmartCoffeeMachine.WebAPI.Models;
using System;
using System.Threading.Tasks;

namespace SmartCoffeeMachine.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmartCoffeeController : ControllerBase
    {
        /// <summary>
        ///     Endpoint to publish an event/message.
        /// </summary>
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly ILogger<SmartCoffeeController> _logger;
        
        
        public SmartCoffeeController(IPublishEndpoint publishEndpoint,
                                     ILogger<SmartCoffeeController> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // POST: api/Order
        /// <summary>
        ///     Request to make a new coffee 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MakeCoffeeCommand command)
        {
            Guid orderId = Guid.NewGuid();
            MakeCoffeeResponse response = new MakeCoffeeResponse()
            {
                OrderId = orderId
            };

            await _publishEndpoint.Publish<IOrderAccepted>(new
            {
                OrderId = orderId,
                CoffeeType = command.CoffeeType
            });

            return Ok(response);
        }

    }
}
