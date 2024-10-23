using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Warehouses.Domain;

public sealed class WarehousesDomainService([FromKeyedServices("warehouses")] IRepository repository) : IWarehousesDomainService
{
	public async Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken)
	{
		var aggregate = Entities.Availability.CreateAvailability(beerId, beerName, quantity);
		await repository.InsertAsync(aggregate, cancellationToken);
	}
}