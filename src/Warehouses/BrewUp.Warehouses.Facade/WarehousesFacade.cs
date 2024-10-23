using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainModel;
using BrewUp.Warehouses.Domain;
using BrewUp.Warehouses.ReadModel.Services;
using BrewUp.Warehouses.SharedKernel.Contracts;

namespace BrewUp.Warehouses.Facade;

public sealed class WarehousesFacade(IWarehousesDomainService warehousesDomainService, IAvailabilityQueryService availabilityQueryService) : IWarehousesFacade
{
	public async Task<PagedResult<BeerAvailabilityJson>> GetAvailabilityAsync(Guid beerId, CancellationToken cancellationToken)
	{
		return await availabilityQueryService.GetAvailabilityAsync(beerId, cancellationToken);
	}

	public async Task SetAvailabilityAsync(SetAvailabilityJson availability, CancellationToken cancellationToken)
	{
		await warehousesDomainService.UpdateAvailabilityDueToProductionOrderAsync(
			new BeerId(new Guid(availability.BeerId)), new BeerName(availability.BeerName), availability.Quantity,
			cancellationToken);
	}
}