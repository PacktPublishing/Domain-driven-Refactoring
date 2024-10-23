namespace BrewUp.Rest.Modules;

public sealed class HstsModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		return builder.Services;
	}

	public WebApplication Configure(WebApplication app)
	{
		app.UseHsts();
		return app;
	}
}