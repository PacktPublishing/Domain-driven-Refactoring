using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Muflone.Persistence;

namespace BrewUp.Sales.Facade;

public sealed class SalesFacade(IServiceBus serviceBus,
	IQueries<SalesOrder> orderQueries) : ISalesFacade
{
	public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
	{
		if (body.SalesOrderId.Equals(string.Empty))
			body = body with { SalesOrderId = Guid.NewGuid().ToString() };

		CreateSalesOrder command = new(new SalesOrderId(new Guid(body.SalesOrderId)),
						Guid.NewGuid(), new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate),
									new CustomerId(body.CustomerId), new CustomerName(body.CustomerName), body.Rows);
		await serviceBus.SendAsync(command, cancellationToken);

		return body.SalesOrderId;
	}

	public async Task<PagedResult<SalesOrderJson>> GetOrdersAsync(CancellationToken cancellationToken)
	{
		var salesOrders = await orderQueries.GetByFilterAsync(null, 0, 100, cancellationToken);

		return salesOrders.TotalRecords > 0
			? new PagedResult<SalesOrderJson>(salesOrders.Results.Select(r => r.ToJson()), salesOrders.Page, salesOrders.PageSize, salesOrders.TotalRecords)
			: new PagedResult<SalesOrderJson>(Enumerable.Empty<SalesOrderJson>(), 0, 0, 0);
	}
}