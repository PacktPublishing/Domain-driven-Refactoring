using BrewUp.DomainModel.Services;
using BrewUp.ReadModel.Sales.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services;

public static class SalesOrderService
{
	public static async Task<Results<Created, NotFound>> HandleCreateSalesOrder(ISalesOrderService salesOrderService, SalesOrderJson body, CancellationToken cancellationToken)
	{
		await salesOrderService.CreateSalesOrderAsync(new SalesOrderId(new Guid(body.SalesOrderId)),
			new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate),
			new CustomerId(body.CustomerId), new CustomerName(body.CustomerName),
			body.Rows, cancellationToken);

		return TypedResults.Created($"v1/sales/{body.SalesOrderId}");
	}

	public static async Task<Results<Ok<PagedResult<SalesOrderJson>>, NotFound>> HandleGetOrders(ISalesQueryService salesQueryService, CancellationToken cancellationToken)
	{
		var orders = await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
		return TypedResults.Ok(orders);
	}
}