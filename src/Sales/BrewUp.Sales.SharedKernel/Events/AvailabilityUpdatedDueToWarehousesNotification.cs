using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Sales.SharedKernel.Events;

public class AvailabilityUpdatedDueToWarehousesNotification(BeerId aggregateId, Guid commitId, BeerName beerName,
	Quantity quantity) : DomainEvent(aggregateId, commitId)
{
	public readonly BeerId BeerId = aggregateId;
	public readonly BeerName BeerName = beerName;
	public readonly Quantity Quantity = quantity;
}