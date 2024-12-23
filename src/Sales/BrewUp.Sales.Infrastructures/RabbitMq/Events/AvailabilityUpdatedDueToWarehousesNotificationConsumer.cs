using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMq.Events;

public sealed class AvailabilityUpdatedDueToWarehousesNotificationConsumer(IAvailabilityService availabilityService,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<AvailabilityUpdatedDueToWarehousesNotification>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<AvailabilityUpdatedDueToWarehousesNotification>> HandlersAsync { get; } = new List<IDomainEventHandlerAsync<AvailabilityUpdatedDueToWarehousesNotification>>
	{
		new AvailabilityUpdatedDueToWarehousesNotificationEventHandler(loggerFactory, availabilityService)
	};
}