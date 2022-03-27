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
        private readonly ILogger<MakeCoffeeConsumer> _logger;

        public MakeCoffeeConsumer(ILogger<MakeCoffeeConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            _logger.LogInformation($"{nameof(MakeCoffeeConsumer)}: Starting to make coffee ({context.Message.CoffeeType}) for order id = {context.Message.OrderId}.");

            await Task.Delay(3000);

            _logger.LogInformation($"{nameof(MakeCoffeeConsumer)}: Completed to make coffee ({context.Message.CoffeeType}) for order id = {context.Message.OrderId}.");

        }
    }
}
