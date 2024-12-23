using BrewUp.Sales.Domain.Entities;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;

namespace BrewUp.Sales.Domain.Helpers;

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

	internal static ReadModel.Dtos.SalesOrder MapToReadModel(this SalesOrder salesOrder)
	{
		return ReadModel.Dtos.SalesOrder.CreateSalesOrder(salesOrder._salesOrderId, salesOrder._salesOrderNumber,
						salesOrder._customerId, salesOrder._customerName, salesOrder._orderDate,
									salesOrder._rows.Select(r => new SalesOrderRowJson
									{
										BeerId = r._beerId.Value,
										BeerName = r._beerName.Value,
										Quantity = r._quantity,
										Price = r._beerPrice
									}));
	}
}