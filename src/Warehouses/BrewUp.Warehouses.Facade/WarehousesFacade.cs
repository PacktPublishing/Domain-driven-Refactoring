using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.SharedKernel.Commands;
using BrewUp.Warehouses.SharedKernel.Contracts;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Facade;

public sealed class WarehousesFacade(IServiceBus serviceBus,
  IQueries<BrewUp.Warehouses.ReadModel.Dtos.Availability> queries) : IWarehousesFacade
{

  public async Task SetAvailabilityAsync(SetAvailabilityJson availability, CancellationToken cancellationToken)
  {
    cancellationToken.ThrowIfCancellationRequested();

    var chkAvailability = await queries.GetByIdAsync(availability.BeerId, cancellationToken);
    if (chkAvailability is not null)
    {
      CreateAvailabilityDueToProductionOrder command =
        new(new BeerId(new Guid(availability.BeerId)), Guid.NewGuid(), new BeerName(availability.BeerName),
          availability.Quantity);

      await serviceBus.SendAsync(command, cancellationToken).ConfigureAwait(false);

      return;
    }

    UpdateAvailabilityDueToProductionOrder updateAvailabilityDueToProductionOrder =
    new(new BeerId(new Guid(availability.BeerId)), Guid.NewGuid(), new BeerName(availability.BeerName),
      availability.Quantity);

    await serviceBus.SendAsync(updateAvailabilityDueToProductionOrder, cancellationToken);
  }
}