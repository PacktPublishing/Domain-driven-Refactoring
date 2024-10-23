using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BrewUp.Rest.Modules;

public sealed class HealthModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 10;

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		builder.Services.AddHealthChecks()
			.AddMongoDb(builder.Configuration["BrewUp:MongoDbSettings:ConnectionString"]!, name: "MongoDB", failureStatus: HealthStatus.Unhealthy);

		return builder.Services;
	}

	public WebApplication Configure(WebApplication app)
	{
		app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
		return app;
	}
}