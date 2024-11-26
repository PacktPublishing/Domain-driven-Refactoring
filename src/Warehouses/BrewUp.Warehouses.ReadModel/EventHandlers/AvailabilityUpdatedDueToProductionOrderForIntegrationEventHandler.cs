using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;

namespace BrewUp.Warehouses.ReadModel.EventHandlers;

public class AvailabilityUpdatedDueToProductionOrderForIntegrationEventHandler(ILoggerFactory loggerFactory,
	IEventBus eventBus) : DomainEventHandlerBase<AvailabilityUpdatedDueToProductionOrder>(loggerFactory)
{
	public override async Task HandleAsync(AvailabilityUpdatedDueToProductionOrder @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		AvailabilityUpdatedForNotification availabilityUpdatedForNotification = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
		await eventBus.PublishAsync(availabilityUpdatedForNotification, cancellationToken);
	}
}