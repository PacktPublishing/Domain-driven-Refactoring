using BrewUp.Shared.CustomTypes;

namespace BrewUp.Warehouses.Domain;

public interface IWarehousesDomainService
{
	Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken);
}