using BrewUp.Shared.CustomTypes;

namespace BrewUp.Sales.Domain.Entities;

public class SalesOrderRow
{
	internal readonly BeerId _beerId;
	internal readonly BeerName _beerName;

	internal readonly Quantity _quantity;
	internal readonly Price _beerPrice;

	protected SalesOrderRow()
	{
	}

	internal static SalesOrderRow CreateSalesOrderRow(BeerId beerId, BeerName beerName, Quantity quantity,
		Price price)
	{
		return new SalesOrderRow(beerId, beerName, quantity, price);
	}

	private SalesOrderRow(BeerId beerId, BeerName beerName, Quantity quantity, Price price)
	{
		_beerId = beerId;
		_beerName = beerName;
		_quantity = quantity;
		_beerPrice = price;
	}
}