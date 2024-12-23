using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.Infrastructures.MongoDb.Readmodel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Warehouses.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddWarehousesMongoDb(this IServiceCollection services,
		MongoDbSettings mongoDbSettings)
	{
		services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));

		services.AddSingleton<IEventStorePositionRepository>(x =>
			new EventStorePositionRepository(x.GetRequiredService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

		services.AddKeyedScoped<IPersister, WarehousesPersister>("warehouses");

		return services;
	}
}