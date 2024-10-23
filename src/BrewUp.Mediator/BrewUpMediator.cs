using BrewUp.Sales.Facade;
using BrewUp.Shared.Contracts;
using BrewUp.Warehouses.Facade;

namespace BrewUp.Mediator;

public class BrewUpMediator(ISalesFacade salesFacade, IWarehousesFacade warehouseFacade) : IBrewUpMediator
{
	public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
	{
		List<BeerAvailabilityJson> availabilities = new();

		foreach (var row in body.Rows)
		{
			var availability = await warehouseFacade.GetAvailabilityAsync(row.BeerId, cancellationToken);
			if (availability.TotalRecords > 0)
				availabilities.Add(availability.Results.First());
		}

		// Prepare the list of rows that are available for sale
		List<SalesOrderRowJson> rowsForSale = (from row in body.Rows
																					 let beerAvailability = availabilities.Find(a => a.BeerId == row.BeerId.ToString())
																					 where beerAvailability != null && beerAvailability.Availability.Available >= row.Quantity.Value
																					 select row).ToList();

		if (rowsForSale.Count == 0)
		{
			return "No beer available for sale";
		}

		body = body with { Rows = rowsForSale };
		return await salesFacade.CreateOrderAsync(body, cancellationToken);
	}
}