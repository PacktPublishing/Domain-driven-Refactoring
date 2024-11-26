namespace BrewUp.Rest.Modules;

public sealed class CorsModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", corsBuilder =>
				corsBuilder.AllowAnyMethod()
					.AllowAnyOrigin()
					.AllowAnyHeader());
		});

		return builder.Services;
	}

	public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}