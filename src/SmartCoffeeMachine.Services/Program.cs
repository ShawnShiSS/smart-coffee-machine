using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SmartCoffeeMachine.MessageComponents.Consumers;
using SmartCoffeeMachine.MessageComponents.StateMachines;
using System.Threading.Tasks;

namespace SmartCoffeeMachine.Services
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Build a config object, using env vars and JSON providers.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // Add a messsage broker using MassTransit
                    services.AddMassTransit(busRegistrationConfigurator =>
                    {
                        busRegistrationConfigurator.AddConsumer<MakeCoffeeConsumer>();

                        // Azure Service Bus message broker
                        busRegistrationConfigurator.UsingAzureServiceBus((context, cfg) =>
                        {
                            string azureServiceBusConnectionString = config["AzureServiceBusConnectionString"];
                            cfg.Host(azureServiceBusConnectionString);

                            // Configure the Azure Service Bus topics, subsciptions, and the underlying queues for each subscription
                            cfg.ConfigureEndpoints(context);
                        });

                        // Add Saga State Machines
                        const string redisConfigurationString = "127.0.0.1";
                        // Passing a definition allows us to configure 
                        busRegistrationConfigurator.AddSagaStateMachine<OrderStateMachine, OrderState>(typeof(OrderStateMachineDefinition))
                           // Redis repository to store state instances. By default, redis runs on localhost.
                           .RedisRepository(r =>
                           {
                               r.DatabaseConfiguration(redisConfigurationString);

                               r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                           });


                    });
                })
                .Build()
                .RunAsync();
        }
    }
}
