using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.Entities;

namespace BrewUp.DomainModel.Entities.Warehouses;

public class Availability : AggregateRoot
{
	internal BeerId _beerId = default!;
	internal BeerName _beerName = default!;
	internal Quantity _quantity = default!;

	protected Availability()
	{
	}

	internal static Availability CreateAvailability(BeerId beerId, BeerName beerName, Quantity quantity)
	{
		return new Availability(beerId, beerName, quantity);
	}

	private Availability(BeerId beerId, BeerName beerName, Quantity quantity)
	{
		Id = beerId.Value.ToString();

		_beerId = beerId;
		_beerName = beerName;
		_quantity = quantity;
	}
}