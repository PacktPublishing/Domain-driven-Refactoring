using BrewUp.Infrastructure.MongoDb;
using BrewUp.Sales.Facade;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Rest.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 90;

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		builder.Services.AddMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);

		builder.Services.AddSalesInfrastructure();
		builder.Services.AddWarehousesInfrastructure();

		return builder.Services;
	}

	WebApplication IModule.Configure(WebApplication app) => app;
}