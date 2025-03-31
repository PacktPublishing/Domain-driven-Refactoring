using BrewUp.Sales.Domain.Entities;
using BrewUp.Shared.DomainModel;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace BrewUp.Sales.Infrastructures;

public static class MongoDbHelper
{
	public static IServiceCollection AddSalesMongoDb(this IServiceCollection services)
	{
		services.AddKeyedScoped<IRepository, SalesRepository>("sales");

		return services;
	}
}