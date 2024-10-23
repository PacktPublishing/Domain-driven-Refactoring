using BrewUp.Shared.CustomTypes;

namespace BrewUp.DomainModel.Services;

public interface IWarehouseService
{
	Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken);
}