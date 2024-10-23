using BrewUp.Shared.CustomTypes;

namespace BrewUp.DomainModel.Entities.Sales;

public class SalesOrderRow
{
	internal readonly BeerId _beerId = default!;
	internal readonly BeerName _beerName = default!;

	internal readonly Quantity _quantity = default!;
	internal readonly Price _beerPrice = default!;

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