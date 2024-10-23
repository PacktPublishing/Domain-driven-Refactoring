using BrewUp.Shared.Contracts;
using BrewUp.Shared.Entities;

namespace BrewUp.ReadModel.Sales.Services;

public interface ISalesQueryService
{
	Task<PagedResult<SalesOrderJson>> GetSalesOrdersAsync(int page, int pageSize, CancellationToken cancellationToken);
}