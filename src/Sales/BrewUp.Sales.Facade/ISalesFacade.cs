using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;

namespace BrewUp.Sales.Facade;

public interface ISalesFacade
{
	Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken);
	Task<PagedResult<SalesOrderJson>> GetOrdersAsync(CancellationToken cancellationToken);
}