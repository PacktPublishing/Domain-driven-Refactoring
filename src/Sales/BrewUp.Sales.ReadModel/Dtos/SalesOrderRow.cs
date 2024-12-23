using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.ReadModel.Dtos;

public class SalesOrderRow
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;
	public Quantity Quantity { get; set; } = default!;
	public Price Price { get; set; } = default!;

	internal SalesOrderRowJson ToJson => new()
	{
		BeerId = new Guid(BeerId),
		BeerName = BeerName,
		Quantity = Quantity,
		Price = Price
	};
}