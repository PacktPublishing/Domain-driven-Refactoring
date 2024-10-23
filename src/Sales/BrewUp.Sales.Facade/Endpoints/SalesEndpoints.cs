using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesEndpoints
{
	public static WebApplication MapSalesEndpoints(this WebApplication app)
	{
		var group = app.MapGroup("/v1/sales/")
			.WithTags("Sales");

		group.MapGet("/", HandleGetOrders)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("GetSalesOrders");

		return app;
	}

	public static async Task<IResult> HandleGetOrders(
		ISalesFacade salesUpFacade,
		CancellationToken cancellationToken)
	{
		var orders = await salesUpFacade.GetOrdersAsync(cancellationToken);

		return Results.Ok(orders);
	}
}