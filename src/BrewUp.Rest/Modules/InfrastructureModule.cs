using BrewUp.Infrastructure;
using BrewUp.Infrastructure.MongoDb;
using BrewUp.Infrastructure.RabbitMq;
using BrewUp.Sales.Facade;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Rest.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 90;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddInfrastructure(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!,
			builder.Configuration.GetSection("BrewUp:EventStore").Get<EventStoreSettings>()!);

		var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMQ")
			.Get<RabbitMqSettings>()!;

		builder.Services.AddSalesInfrastructure(rabbitMqSettings);
		builder.Services.AddWarehousesInfrastructure(rabbitMqSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}