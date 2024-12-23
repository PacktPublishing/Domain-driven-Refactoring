using Microsoft.OpenApi.Models;

namespace BrewUp.Sales.Rest.Modules;

public sealed class SwaggerModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
		{
			Description = "BrewUp Sales",
			Title = "BrewUp Sales API",
			Version = "v1",
			Contact = new OpenApiContact
			{
				Name = "BrewUp Sales"
			}
		}));

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}