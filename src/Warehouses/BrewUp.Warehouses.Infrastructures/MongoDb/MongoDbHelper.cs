using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddWarehousesMongoDb(this IServiceCollection services)
	{
		services.AddKeyedScoped<IPersister, WarehousesPersister>("warehouses");

		return services;
	}
}