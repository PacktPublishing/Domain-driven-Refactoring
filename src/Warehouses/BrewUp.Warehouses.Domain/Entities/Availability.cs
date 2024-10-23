using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Warehouses.Domain.Entities;

public class Availability : AggregateRoot
{
	internal BeerId _beerId;
	internal BeerName _beerName;
	internal Quantity _quantity;

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