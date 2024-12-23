using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class SalesOrderService(ILoggerFactory loggerFactory, [FromKeyedServices("sales")] IPersister persister, IQueries<SalesOrder> queries)
	: ServiceBase(loggerFactory, persister), ISalesOrderService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, CustomerId customerId,
		CustomerName customerName, OrderDate orderDate, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrder = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, customerId, customerName, orderDate, rows);
			await Persister.InsertAsync(salesOrder, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error creating sales order");
			throw;
		}
	}

	public async Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrders = await queries.GetByFilterAsync(null, page, pageSize, cancellationToken);

			return salesOrders.TotalRecords > 0
				? new PagedResult<SalesOrderJson>(salesOrders.Results.Select(r => r.ToJson()), salesOrders.Page, salesOrders.PageSize, salesOrders.TotalRecords)
				: new PagedResult<SalesOrderJson>(Enumerable.Empty<SalesOrderJson>(), 0, 0, 0);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error reading SalesOrders");
			throw;
		}
	}

	public async Task CompleteSalesOrderAsync(SalesOrderId eventSalesOrderId, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrder = await Persister.GetByIdAsync<SalesOrder>(eventSalesOrderId.Value.ToString(), cancellationToken);
			salesOrder.CompleteOrder();
			await Persister.UpdateAsync(salesOrder, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error completing SalesOrders");
			throw;
		}
	}
}