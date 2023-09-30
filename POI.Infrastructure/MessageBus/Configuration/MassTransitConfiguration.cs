using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using POI.Infrastructure.MessageBus.Consumers;
using POI.Infrastructure.MessageBus.Messages;

namespace POI.Infrastructure.MessageBus.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void ConfigureMassTransit(this IServiceCollection services, string rabbitMqUrl)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(rabbitMqUrl));

                    cfg.UseMessageRetry(retryConfig => retryConfig.Interval(3, TimeSpan.FromSeconds(10))); // Try 3 times, 10 seconds apart

                    // ConfigureEndpoint<SampleMessageConsumer, SampleMessage>(cfg, "sample-queue",context);
                    ConfigureEndpoint<PoiCreatedConsumer, PoiCreatedMessage>(cfg, "poi-created-queue",context);
                    ConfigureEndpoint<PoiUpdatedConsumer, PoiUpdatedMessage>(cfg, "poi-updated-queue",context);
                    ConfigureEndpoint<PoiDeletedConsumer, PoiDeletedMessage>(cfg, "poi-deleted-queue",context);
                    
                });
            });

            services.AddMassTransitHostedService();
        }

        private static void ConfigureEndpoint<TConsumer, TMessage>(IRabbitMqBusFactoryConfigurator cfg, string queueName, IBusRegistrationContext context)
            where TConsumer : class, IConsumer<TMessage>
            where TMessage : class
        {
            cfg.ReceiveEndpoint(queueName, e =>
            {
                e.Consumer<TConsumer>(context); // Use ConfigureConsumer instead of Consumer
            });
        }
    }
}