using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Shared.Messages.Sagas;

public sealed class BeerAvailabilityCommunicated : IntegrationEvent
{
	public readonly BeerId BeerId;
	public readonly Availability Availability;

	public BeerAvailabilityCommunicated(BeerId aggregateId, Guid correlationId, Availability availability) : base(aggregateId, correlationId)
	{
		BeerId = aggregateId;
		Availability = availability;
	}
}