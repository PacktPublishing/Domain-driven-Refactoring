using BrewUp.Warehouses.Domain.CommandHandlers;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Warehouses.Infrastructures.RabbitMq.Commands;

public sealed class CreateAvailabilityDueToProductionOrderConsumer(IRepository repository,
  IRabbitMQConnectionFactory connectionFactory, ILoggerFactory loggerFactory)
  : CommandConsumerBase<CreateAvailabilityDueToProductionOrder>(repository, connectionFactory, loggerFactory)
{
  protected override ICommandHandlerAsync<CreateAvailabilityDueToProductionOrder> HandlerAsync { get; } =
    new CreateAvailabilityDueToProductionOrderCommandHandler(repository, loggerFactory);
}