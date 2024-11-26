using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Sales.Acl;

public sealed class AvailabilityUpdatedForNotificationEventHandler(ILoggerFactory loggerFactory, IServiceBus serviceBus) : IntegrationEventHandlerAsync<AvailabilityUpdatedForNotification>(loggerFactory)
{
	public override async Task HandleAsync(AvailabilityUpdatedForNotification @event,
		CancellationToken cancellationToken = new())
	{
		cancellationToken.ThrowIfCancellationRequested();

		var correlationId =
			new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);

		UpdateAvailabilityDueToWarehousesNotification command = new(@event.BeerId, correlationId, @event.BeerName, @event.Quantity);
		await serviceBus.SendAsync(command, cancellationToken);
	}
}