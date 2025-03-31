using BrewUp.Warehouses.Domain.Entities;

namespace BrewUp.Warehouses.Domain;

public static class DomainHelper
{	
	internal static SharedKernel.Entities.Availability MapToSharedDto(this Availability availability)
	{
		return SharedKernel.Entities.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}