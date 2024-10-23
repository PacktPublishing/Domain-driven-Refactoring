using BrewUp.DomainModel.Entities.Sales;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.DomainModel.Services;

public sealed class SalesOrderService(
	[FromKeyedServices("sale")] IRepository saleRepository, 
	[FromKeyedServices("warehouse")] IRepository warehouseRepository) : ISalesOrderService
{
	public async Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, IEnumerable<SalesOrderRowJson> rows, CancellationToken cancellationToken)
	{
		List<SalesOrderRowJson> beersAvailable = new();
		foreach (var row in rows)
		{
			var availability = await warehouseRepository.GetByIdAsync<Entities.Warehouses.Availability>(row.BeerId.ToString(), cancellationToken);
			if (availability!=null)
				beersAvailable.Add(row);
		}

		var aggregate = SalesOrder.CreateSalesOrder(salesOrderId, salesOrderNumber, orderDate, customerId, customerName, beersAvailable);

		await saleRepository.InsertAsync(aggregate.MapToReadModel(), cancellationToken);
	}
}