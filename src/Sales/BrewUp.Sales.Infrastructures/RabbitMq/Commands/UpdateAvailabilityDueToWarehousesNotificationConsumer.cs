using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMq.Commands;

public class UpdateAvailabilityDueToWarehousesNotificationConsumer(IRepository repository,
		IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<UpdateAvailabilityDueToWarehousesNotification>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<UpdateAvailabilityDueToWarehousesNotification> HandlerAsync { get; } = new UpdateAvailabilityDueToWarehousesNotificationCommandHandler(repository, loggerFactory);
}