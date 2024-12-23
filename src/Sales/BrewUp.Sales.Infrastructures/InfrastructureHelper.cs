using BrewUp.Sales.Infrastructures.MongoDb;
using BrewUp.Sales.Infrastructures.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Saga.Persistence.MongoDb;

namespace BrewUp.Sales.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		EventStoreSettings eventStoreSettings)
	{
		services.AddSalesMongoDb(mongoDbSettings);
		services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName));

		services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

		services.AddRabbitMqForSalesModule(rabbitMqSettings);

		return services;
	}
}