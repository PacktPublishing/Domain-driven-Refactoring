using Serilog;

namespace BrewUp.Rest.Modules;

public class SerilogModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection Register(WebApplicationBuilder builder)
	{
		var logger = new LoggerConfiguration()
				.ReadFrom.Configuration(builder.Configuration)
				.CreateLogger();
		builder.Logging.AddSerilog(logger);
		return builder.Services;
	}

	public WebApplication Configure(WebApplication app)
	{
		return app;
	}
}