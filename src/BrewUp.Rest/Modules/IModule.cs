namespace BrewUp.Rest.Modules;

public interface IModule
{
	bool IsEnabled { get; }
	int Order { get; }

	IServiceCollection Register(WebApplicationBuilder builder);
	WebApplication Configure(WebApplication app);
}