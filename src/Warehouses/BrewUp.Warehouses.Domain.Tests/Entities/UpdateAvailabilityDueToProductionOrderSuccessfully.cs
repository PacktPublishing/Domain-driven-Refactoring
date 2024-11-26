using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.SharedKernel.Commands;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace BrewUp.Warehouses.Domain.Tests.Entities;

public class UpdateAvailabilityDueToProductionOrderSuccessfully : CommandSpecification<UpdateAvailabilityDueToProductionOrder>
{
	private readonly BeerId _beerId = new(Guid.NewGuid());
	private readonly BeerName _beerName = new("Muflone IPA");
	private readonly Quantity _quantity = new(100, "Lt");

	private readonly Guid _correlationId = Guid.NewGuid();

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override UpdateAvailabilityDueToProductionOrder When()
	{
		return new UpdateAvailabilityDueToProductionOrder(_beerId, _correlationId, _beerName, _quantity);
	}

	protected override ICommandHandlerAsync<UpdateAvailabilityDueToProductionOrder> OnHandler()
	{
		return new UpdateAvailabilityDueToProductionOrderCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new AvailabilityUpdatedDueToProductionOrder(_beerId, _correlationId, _beerName, _quantity);
	}
}