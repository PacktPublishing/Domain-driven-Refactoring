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
			app.UseSwaggerUI(x =>
			{
				//La versione deve essere identica con quella specificata nel modulo moduels\swagger.cs o da errore con il json
				x.SwaggerEndpoint("v1.0/swagger.json", "Catalog API v1.0");
				x.RoutePrefix = "swagger/products";
			});
			app.UseDeveloperExceptionPage();
		}
		return app;
	}

}