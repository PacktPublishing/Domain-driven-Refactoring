using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainModel;
using BrewUp.Warehouses.SharedKernel.Contracts;

namespace BrewUp.Warehouses.Facade;

public interface IWarehousesFacade
{
	Task<PagedResult<BeerAvailabilityJson>> GetAvailabilityAsync(Guid beerId, CancellationToken cancellationToken);
	Task SetAvailabilityAsync(SetAvailabilityJson availability, CancellationToken cancellationToken);
}