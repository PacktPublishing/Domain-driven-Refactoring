using BrewUp.Warehouses.Domain.Entities;
using BrewUp.Warehouses.SharedKernel.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public sealed class CreateAvailabilityDueToProductionOrderCommandHandler(
  IRepository repository, ILoggerFactory loggerFactory)
  : CommandHandlerBaseAsync<CreateAvailabilityDueToProductionOrder>(repository, loggerFactory)
{
  public override async Task ProcessCommand(CreateAvailabilityDueToProductionOrder command, CancellationToken cancellationToken = default)
  {
    try
    {
      var aggregate = Availability.CreateAvailability(command.BeerId, command.BeerName, command.Quantity, command.MessageId);

      await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
    catch (Exception e)
    {
      // I'm lazy ... I should raise an event here
      Console.WriteLine(e);
      throw;
    }
  }
}