using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Domain;

public sealed class SalesDomainService([FromKeyedServices("sales")] IRepository repository) : ISalesDomainService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		var aggregate = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, rows);

		await repository.InsertAsync(aggregate, cancellationToken);
	}
}