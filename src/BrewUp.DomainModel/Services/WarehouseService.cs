using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.DomainModel.Services;

public sealed class WarehouseService([FromKeyedServices("warehouse")] IRepository repository) : IWarehouseService
{
	public async Task UpdateAvailabilityDueToProductionOrderAsync(BeerId beerId, BeerName beerName, Quantity quantity,
		CancellationToken cancellationToken)
	{
		var aggregate = Entities.Warehouses.Availability.CreateAvailability(beerId, beerName, quantity);
		await repository.InsertAsync(aggregate.MapToReadModel(), cancellationToken);
	}
}