using BrewUp.Mediator;
using BrewUp.Mediator.Endpoints;

namespace BrewUp.Rest.Modules;

public class BrewUpModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection Register(WebApplicationBuilder builder) => builder.Services.AddMediator();

	WebApplication IModule.Configure(WebApplication app) => app.MapMediatorEndpoints();
	
}