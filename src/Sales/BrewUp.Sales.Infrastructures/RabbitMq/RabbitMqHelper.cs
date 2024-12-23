using BrewUp.Sales.Infrastructures.RabbitMq.Commands;
using BrewUp.Sales.Infrastructures.RabbitMq.Events;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUp.Sales.Infrastructures.RabbitMq;

public static class RabbitMqHelper
{
	public static IServiceCollection AddRabbitMqForSalesModule(this IServiceCollection services,
		RabbitMqSettings rabbitMqSettings)
	{
		var serviceProvider = services.BuildServiceProvider();
		var repository = serviceProvider.GetRequiredService<IRepository>();
		var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

		var rabbitMqConfiguration = new RabbitMQConfiguration(rabbitMqSettings.Host, rabbitMqSettings.Username,
			rabbitMqSettings.Password, rabbitMqSettings.ExchangeCommandName, rabbitMqSettings.ExchangeEventName);
		var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMqConfiguration, loggerFactory);

		services.AddMufloneTransportRabbitMQ(loggerFactory, rabbitMqConfiguration);

		serviceProvider = services.BuildServiceProvider();
		var consumers = serviceProvider.GetRequiredService<IEnumerable<IConsumer>>();
		consumers = consumers.Concat(new List<IConsumer>
		{
			new CreateSalesOrderConsumer(repository,
				mufloneConnectionFactory,
				loggerFactory),
			new SalesOrderCreatedConsumer(serviceProvider.GetRequiredService<ISalesOrderService>(),
				serviceProvider.GetRequiredService<IEventBus>(),
				mufloneConnectionFactory, loggerFactory),

			new AvailabilityUpdatedForNotificationConsumer(serviceProvider.GetRequiredService<IServiceBus>(),
				mufloneConnectionFactory,
				loggerFactory),

			new UpdateAvailabilityDueToWarehousesNotificationConsumer(repository, mufloneConnectionFactory,
				loggerFactory),
			new AvailabilityUpdatedDueToWarehousesNotificationConsumer(serviceProvider.GetRequiredService<IAvailabilityService>(),
								mufloneConnectionFactory,
												loggerFactory)
		});

		services.AddMufloneRabbitMQConsumers(consumers);

		return services;
	}
}