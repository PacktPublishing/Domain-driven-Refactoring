using BrewUp.Sales.Facade;
using BrewUp.Sales.Facade.Endpoints;

namespace BrewUp.Rest.Modules;

public class SalesModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public IServiceCollection Register(WebApplicationBuilder builder) => builder.Services.AddSales();

	WebApplication IModule.Configure(WebApplication app) => app.MapSalesEndpoints();
}