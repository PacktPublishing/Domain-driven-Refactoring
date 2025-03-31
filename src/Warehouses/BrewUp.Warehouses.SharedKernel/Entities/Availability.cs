using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Warehouses.SharedKernel.Entities;

public class Availability : EntityBase
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;

	public Quantity Quantity { get; set; } = new(0, string.Empty);

	protected Availability()
	{
	}

	public static Availability Create(BeerId beerId, BeerName beerName, Quantity quantity)
	{
		return new Availability(beerId.Value.ToString(), beerName.Value, quantity);
	}

	private Availability(string beerId, string beerName, Quantity quantity)
	{
		Id = beerId;

		BeerId = beerId;
		BeerName = beerName;
		Quantity = quantity;
	}

	public BeerAvailabilityJson ToJson() => new(Id, BeerName,
			new Shared.CustomTypes.Availability(0, Quantity.Value, Quantity.UnitOfMeasure));
}