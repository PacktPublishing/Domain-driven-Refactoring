using BrewUp.Warehouses.ReadModel.EventHandlers;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Events;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Events;

public sealed class AvailabilityUpdatedDueToProductionOrderConsumer(IAvailabilityService availabilityService,
		IEventBus eventBus,
		IMufloneConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
	: DomainEventsConsumerBase<AvailabilityUpdatedDueToProductionOrder>(connectionFactory, loggerFactory)
{
	protected override IEnumerable<IDomainEventHandlerAsync<AvailabilityUpdatedDueToProductionOrder>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<AvailabilityUpdatedDueToProductionOrder>>
	{
		new AvailabilityUpdatedDueToProductionOrderEventHandler(loggerFactory, availabilityService),
		new AvailabilityUpdatedDueToProductionOrderForIntegrationEventHandler(loggerFactory, eventBus)
	};
}