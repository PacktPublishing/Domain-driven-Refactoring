using BrewUp.Warehouses.Infrastructures;
using BrewUp.Warehouses.Infrastructures.MongoDb;
using BrewUp.Warehouses.Infrastructures.RabbitMq;

namespace BrewUp.Warehouses.Rest.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 90;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		var mongoDbSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings")
			.Get<MongoDbSettings>()!;

		var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMQ")
			.Get<RabbitMqSettings>()!;

		var eventStoreSettings = builder.Configuration.GetSection("BrewUp:EventStore")
			.Get<EventStoreSettings>()!;

		builder.Services.AddInfrastructure(mongoDbSettings, rabbitMqSettings, eventStoreSettings);

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}