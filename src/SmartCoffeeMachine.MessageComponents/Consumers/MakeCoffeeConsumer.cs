using MassTransit;
using Microsoft.Extensions.Logging;
using SmartCoffeeMachine.MessageContracts;
using System;
using System.Threading.Tasks;

namespace SmartCoffeeMachine.MessageComponents.Consumers
{

    /// <summary>
    ///     Consumer to actually make a coffee.
    /// </summary>
    public class MakeCoffeeConsumer : IConsumer<IOrderAccepted>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<MakeCoffeeConsumer> _logger;

        public MakeCoffeeConsumer(IPublishEndpoint publishEndpoint, 
                                  ILogger<MakeCoffeeConsumer> logger)
        {
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            _logger.LogInformation($"{nameof(MakeCoffeeConsumer)}: Starting to make coffee ({context.Message.CoffeeType}) for order id = {context.Message.OrderId}. UTC: {DateTime.UtcNow}.");

            // Pretend to be doing some long-running tasks, 20 secs.
            await Task.Delay(20000);

            await _publishEndpoint.Publish<IOrderCompleted>(new
            {
                OrderId = context.Message.OrderId,
                TimeStamp = DateTime.UtcNow
            });

            _logger.LogInformation($"{nameof(MakeCoffeeConsumer)}: Completed to make coffee ({context.Message.CoffeeType}) for order id = {context.Message.OrderId}. UTC: {DateTime.UtcNow}.");

        }
    }
}
