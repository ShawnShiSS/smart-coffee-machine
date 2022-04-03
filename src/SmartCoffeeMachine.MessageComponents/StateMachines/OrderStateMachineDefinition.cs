using MassTransit;

namespace SmartCoffeeMachine.MessageComponents.StateMachines
{
    /// <summary>
    ///     State machine is the behavior, the data of the saga is the OrderState.
    /// </summary>
    public class OrderStateMachineDefinition : SagaDefinition<OrderState>
    {
        public OrderStateMachineDefinition()
        {
            // Proccess 8 messages concurrently at max at one time.
            // Multiple instances would only pre-load 8 messages.
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<OrderState> sagaConfigurator)
        {
            // Any failed message will be locked inside the broker and is not available to other consumers.
            // Any failed message will be kept in memory and block any processing message slots, e.g. pre-fetch slots.
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 5000, 10000));

            // Prevent any messages or events published from this state machine from going out. E.g., this state machine will stop publishing events.
            sagaConfigurator.UseInMemoryOutbox();
        }
    }
}
