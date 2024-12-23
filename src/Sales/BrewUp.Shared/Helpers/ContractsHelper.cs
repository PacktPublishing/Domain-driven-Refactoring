using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Shared.Helpers;

public static class ContractsHelper
{
	public static IEnumerable<BeerAvailabilityJson> ToBeerAvailabilities(this IEnumerable<SalesOrderRowJson> rows)
		=> rows.Select(row => new BeerAvailabilityJson(row.BeerId.ToString(), row.BeerName,
			new Availability(row.Quantity.Value, 0, row.Quantity.UnitOfMeasure)));
}