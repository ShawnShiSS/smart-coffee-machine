using MassTransit;
using SmartCoffeeMachine.MessageContracts;
using System;

namespace SmartCoffeeMachine.MessageComponents.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            // Events have to be correlated to the state machine instance.
            // E.g., OrderId in the OrderAccepted event will be used to correlate the event to a saga instance. If the instance does not exist, it will get created.
            // If Redis is used as the state machine repository, redis-cli allows us to check an order state by "get orderId"
            Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => OrderCompleted, x => x.CorrelateById(m => m.Message.OrderId));

            Event(() => CheckOrderStatusRequested, x =>
            {
                x.CorrelateById(m => m.Message.OrderId);
                // If state instance does not exist for the order id, we need to return not found.
                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    // Only respond if a response is expected
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<IOrderNotFound>(new
                        {
                            OrderId = context.Message.OrderId
                        });
                    }
                }));
            });

            InstanceState(x => x.CurrentState);

            // All state machines start in the initial state
            // When an OrderAccepted event is published, OrderStateMachine uses it to create an instance of itself, identified by order id.
            Initially(
                When(OrderAccepted)
                    // Assign the properties in the message (in Data) to the saga instance
                    .Then(context => 
                    {
                        context.Instance.Updated = DateTime.UtcNow;

                    })
                    .TransitionTo(Accepted)
            );

            // Handle check order status requests.
            DuringAny(
                When(CheckOrderStatusRequested)
                    .RespondAsync(x => x.Init<IOrderStatus>(new
                    {
                        OrderId = x.Instance.CorrelationId,
                        State = x.Instance.CurrentState
                    }))
            );

            // Handle order completed events.
            DuringAny(
                When(OrderCompleted)
                    .TransitionTo(Completed)
            );

            // Final state is specical, as state machines in final state will be removed
        }

        /// <summary>
        ///     Accepted state.
        /// </summary>
        public State Accepted { get; private set; }

        /// <summary>
        ///  Completed state
        /// </summary>
        public State Completed { get; private set; }

        
        /// <summary>
        ///     On order accepted event.
        /// </summary>
        public Event<IOrderAccepted> OrderAccepted { get; private set; }

        /// <summary>
        ///     On order completed event.
        /// </summary>
        public Event<IOrderCompleted> OrderCompleted { get; private set; }

        /// <summary>
        ///     On check order status event.
        /// </summary>
        public Event<ICheckOrder> CheckOrderStatusRequested { get; private set; }

    }
}
