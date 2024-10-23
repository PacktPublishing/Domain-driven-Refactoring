using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BrewUp.Infrastructure.MongoDb;

public static class MongoDbHelper
{
	public static IServiceCollection AddMongoDb(this IServiceCollection services,
		MongoDbSettings mongoDbSettings)
	{
		services.AddSingleton<IMongoClient>(new MongoClient(mongoDbSettings.ConnectionString));

		return services;
	}
}