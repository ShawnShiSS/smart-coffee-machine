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
        /// <summary>
        ///     Endpoint to check the order status.
        /// </summary>
        private readonly IRequestClient<ICheckOrder> _checkOrderRequestClient;

        private readonly ILogger<SmartCoffeeController> _logger;
        
        
        public SmartCoffeeController(IPublishEndpoint publishEndpoint,
                                     IRequestClient<ICheckOrder> checkOrderRequestClient,
                                     ILogger<SmartCoffeeController> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _checkOrderRequestClient = checkOrderRequestClient ?? throw new ArgumentNullException(nameof(checkOrderRequestClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Database for the order state is owned by another micro-service, so the API can not just talks to the database directly.
        // GET: api/Order/5
        /// <summary>
        ///     Get order status
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetOrderStatus")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<IActionResult> GetOrderStatus(Guid id)
        {
            var (status, notFound) = await _checkOrderRequestClient.GetResponse<IOrderStatus, IOrderNotFound>(new
            {
                OrderId = id
            });

            if (status.IsCompletedSuccessfully)
            {
                var response = await status;
                return Ok(response.Message);
            }

            var notFoundResponse = await notFound;
            return NotFound(notFoundResponse.Message);
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
