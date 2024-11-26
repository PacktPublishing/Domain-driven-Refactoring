using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Warehouses.SharedKernel.Commands;
using BrewUp.Warehouses.SharedKernel.Contracts;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Facade;

public sealed class WarehousesFacade(IServiceBus serviceBus) : IWarehousesFacade
{

	public async Task SetAvailabilityAsync(SetAvailabilityJson availability, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		UpdateAvailabilityDueToProductionOrder updateAvailabilityDueToProductionOrder =
			new(new BeerId(new Guid(availability.BeerId)), Guid.NewGuid(), new BeerName(availability.BeerName),
				availability.Quantity);

		await serviceBus.SendAsync(updateAvailabilityDueToProductionOrder, cancellationToken);
	}
}