using BrewUp.Warehouses.SharedKernel.Contracts;

namespace BrewUp.Warehouses.Facade;

public interface IWarehousesFacade
{
	Task SetAvailabilityAsync(SetAvailabilityJson availability, CancellationToken cancellationToken);
}