using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.ReadModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.Services;

public sealed class AvailabilityService(
  ILoggerFactory loggerFactory,
  [FromKeyedServices("warehouses")] IPersister persister)
  : ServiceBase(loggerFactory, persister), IAvailabilityService
{
  public async Task CreateAvailabilityAsync(BeerId beerId, BeerName beerName, Quantity quantity,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();

    try
    {
      var availability = Dtos.Availability.Create(beerId, beerName, quantity);
      await Persister.InsertAsync(availability, cancellationToken);
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Error updating availability");
      throw;
    }
  }

  public async Task UpdateAvailabilityAsync(BeerId beerId, BeerName beerName, Quantity quantity,
    CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    try
    {
      var availability = await Persister.GetByIdAsync<Dtos.Availability>(beerId.Value, cancellationToken);
      if (availability == null)
      {
        await CreateAvailabilityAsync(beerId, beerName, quantity, cancellationToken);
        return;
      }
      availability.UpdateAvailability(quantity);
      await Persister.UpdateAsync(availability, cancellationToken);
    }
    catch (Exception ex)
    {
      Logger.LogError(ex, "Error updating availability");
      throw;
    }
  }
}