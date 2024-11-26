using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public sealed class AvailabilityUpdatedDueToProductionOrderEventHandler(ILoggerFactory loggerFactory,
		IAvailabilityService availabilityService)
	: DomainEventHandlerBase<AvailabilityUpdatedDueToProductionOrder>(loggerFactory)
{
	public override async Task HandleAsync(AvailabilityUpdatedDueToProductionOrder @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		await availabilityService.UpdateAvailabilityAsync(@event.BeerId, @event.BeerName, @event.Quantity, cancellationToken);
	}
}