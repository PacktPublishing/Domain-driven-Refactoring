using BrewUp.DomainModel.Entities.Sales;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;

namespace BrewUp.DomainModel;

public static class DomainHelper
{
	internal static SalesOrderRow MapToDomainRow(this SalesOrderRowJson json)
	{
		return SalesOrderRow.CreateSalesOrderRow(new BeerId(json.BeerId), new BeerName(json.BeerName), json.Quantity, json.Price);
	}

	internal static IEnumerable<SalesOrderRow> MapToDomainRows(this IEnumerable<SalesOrderRowJson> json)
	{
		return json.Select(r => SalesOrderRow.CreateSalesOrderRow(new BeerId(r.BeerId), new BeerName(r.BeerName), r.Quantity, r.Price));
	}

	internal static Shared.Entities.SalesOrder MapToReadModel(this Entities.Sales.SalesOrder salesOrder)
	{
		return Shared.Entities.SalesOrder.Create(salesOrder._salesOrderId, salesOrder._salesOrderNumber,
						salesOrder._orderDate, salesOrder._customerId, salesOrder._customerName,
									salesOrder._rows.Select(r => new SalesOrderRowJson
									{
										BeerId = r._beerId.Value,
										BeerName = r._beerName.Value,
										Quantity = r._quantity,
										Price = r._beerPrice
									}));
	}

	internal static Shared.Entities.Availability MapToReadModel(this Entities.Warehouses.Availability availability)
	{
		return Shared.Entities.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}