using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Infrastructures.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddSalesMongoDb(this IServiceCollection services)
	{
		services.AddKeyedScoped<IPersister, SalesPersister>("sales");

		return services;
	}
}