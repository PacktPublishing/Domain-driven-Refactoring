using BrewUp.Shared.DomainModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Infrastructures;

public static class MongoDbHelper
{
	public static IServiceCollection AddWarehousesMongoDb(this IServiceCollection services)
	{
		services.AddKeyedScoped<IRepository, WarehousesRepository>("warehouses");

		return services;
	}
}