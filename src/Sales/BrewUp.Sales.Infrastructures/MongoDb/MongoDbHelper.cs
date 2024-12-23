using BrewUp.Sales.Infrastructures.MongoDb.Readmodel;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Sales.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddSalesMongoDb(this IServiceCollection services,
		MongoDbSettings mongoDbSettings)
	{
		services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));

		services.AddSingleton<IEventStorePositionRepository>(x =>
			new EventStorePositionRepository(x.GetRequiredService<ILogger<EventStorePositionRepository>>(), mongoDbSettings));

		services.AddKeyedScoped<IPersister, SalesPersister>("sales");

		return services;
	}
}