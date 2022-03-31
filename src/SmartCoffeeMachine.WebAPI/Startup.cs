using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace SmartCoffeeMachine.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add a messsage broker using MassTransit
            services.AddMassTransit(busRegistrationConfigurator => 
            {
                // Register consumers that will run in the same process as the Web API.
                //busRegistrationConfigurator.AddConsumer<MakeCoffeeConsumer>();

                // In-memory message broker to handle messages 
                //busRegistrationConfigurator.UsingInMemory((context, cfg) =>
                //{
                //    cfg.ConfigureEndpoints(context);
                //});

                // Azure Service Bus message broker
                busRegistrationConfigurator.UsingAzureServiceBus((context, cfg) =>
                {
                    string azureServiceBusConnectionString = Configuration["AzureServiceBusConnectionString"];
                    cfg.Host(azureServiceBusConnectionString);

                    // Configure the Azure Service Bus topics, subsciptions, and the underlying queues for each subscription
                    cfg.ConfigureEndpoints(context);
                });
            });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCoffeeMachine.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartCoffeeMachine.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
