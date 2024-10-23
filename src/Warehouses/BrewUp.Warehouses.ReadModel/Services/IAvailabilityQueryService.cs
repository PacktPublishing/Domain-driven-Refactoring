using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainModel;

namespace BrewUp.Warehouses.ReadModel.Services;

public interface IAvailabilityQueryService
{
	Task<PagedResult<BeerAvailabilityJson>> GetAvailabilityAsync(Guid beerId, CancellationToken cancellationToken);
}