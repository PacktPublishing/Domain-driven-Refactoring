using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Contracts;

namespace BrewUp.Sales.ReadModel.Helpers;

public static class ReadModelHelpers
{
	public static IEnumerable<SalesOrderRow> ToReadModelEntities(this IEnumerable<SalesOrderRowJson> dtos)
	{
		return dtos.Select(dto =>
			new SalesOrderRow
			{
				BeerId = dto.BeerId.ToString(),
				BeerName = dto.BeerName,
				Quantity = dto.Quantity,
				Price = dto.Price
			});
	}
}