using Microsoft.OpenApi.Models;

namespace BrewUp.Rest.Modules;

public sealed class SwaggerModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
		{
			Description = "BrewUp",
			Title = "BrewUp API",
			Version = "v1",
			Contact = new OpenApiContact
			{
				Name = "BrewUp"
			}
		}));

		return builder.Services;
	}

	public WebApplication Configure(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger(option =>
			{
				option.RouteTemplate = "/swagger/products/{documentName}/swagger.{json|yaml}";
			});
			app.UseSwagger(option => { option.RouteTemplate = "documentation/{documentName}/documentation.json"; });
			app.UseSwaggerUI(x =>
			{
				x.SwaggerEndpoint("/documentation/v1/documentation.json", "Intelco API");
				x.RoutePrefix = "documentation";
			});
			app.UseDeveloperExceptionPage();
		}
		return app;
	}
}