using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.SharedKernel.Commands;

public class UpdateAvailabilityDueToWarehousesNotification(BeerId aggregateId, Guid commitId, BeerName beerName,
	Quantity quantity) : Command(aggregateId, commitId)
{
	public readonly BeerId BeerId = aggregateId;
	public readonly BeerName BeerName = beerName;
	public readonly Quantity Quantity = quantity;
}