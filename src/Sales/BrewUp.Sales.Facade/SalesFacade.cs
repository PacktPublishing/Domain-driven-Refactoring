using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Sales.Facade;

public sealed class SalesFacade(ISalesDomainService salesDomainService,
	ISalesQueryService salesQueryService) : ISalesFacade
{
	public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
	{
		if (body.SalesOrderId.Equals(string.Empty))
			body = body with { SalesOrderId = Guid.NewGuid().ToString() };

		await salesDomainService.CreateSalesOrderAsync(new SalesOrderId(new Guid(body.SalesOrderId)),
			new SalesOrderNumber(body.SalesOrderNumber), new OrderDate(body.OrderDate),
			new CustomerId(body.CustomerId), new CustomerName(body.CustomerName),
			body.Rows, cancellationToken);

		return body.SalesOrderId;
	}

	public async Task<PagedResult<SalesOrderJson>> GetOrdersAsync(CancellationToken cancellationToken)
	{
		return await salesQueryService.GetSalesOrdersAsync(0, 30, cancellationToken);
	}
}