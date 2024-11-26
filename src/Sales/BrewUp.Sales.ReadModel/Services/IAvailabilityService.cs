using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;

namespace BrewUp.Sales.ReadModel.Services;

public interface IAvailabilityService
{
	Task UpdateAvailabilityAsync(BeerId beerId, BeerName beerName, Quantity quantity, CancellationToken cancellationToken = default);
}