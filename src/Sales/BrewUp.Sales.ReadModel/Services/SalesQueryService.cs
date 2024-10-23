using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainModel;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class SalesQueryService
	(ILoggerFactory loggerFactory, IQueries<SalesOrder> queries) : ServiceBase(loggerFactory), ISalesQueryService
{
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
}