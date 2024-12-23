using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Infrastructures.RabbitMq.Commands;

public sealed class CreateSalesOrderConsumer(IRepository repository,
		IMufloneConnectionFactory connectionFactory,
		ILoggerFactory loggerFactory)
	: CommandConsumerBase<CreateSalesOrder>(repository, connectionFactory, loggerFactory)
{
	protected override ICommandHandlerAsync<CreateSalesOrder> HandlerAsync { get; } = new CreateSalesOrderCommandHandler(repository, loggerFactory);
}