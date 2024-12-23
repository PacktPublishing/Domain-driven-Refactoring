using BrewUp.Warehouses.Infrastructures.MongoDb;
using BrewUp.Warehouses.Infrastructures.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Saga.Persistence.MongoDb;

namespace BrewUp.Warehouses.Infrastructures;

public static class InfrastructureHelper
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services,
		MongoDbSettings mongoDbSettings,
		RabbitMqSettings rabbitMqSettings,
		EventStoreSettings eventStoreSettings)
	{
		services.AddWarehousesMongoDb(mongoDbSettings);
		services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions(mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName));

		services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

		services.AddRabbitMqForWarehousesModule(rabbitMqSettings);

		return services;
	}
}