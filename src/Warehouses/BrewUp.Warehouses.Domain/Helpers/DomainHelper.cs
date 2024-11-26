namespace BrewUp.Warehouses.Domain.Helpers;

public static class DomainHelper
{
	internal static ReadModel.Dtos.Availability MapToReadModel(this Entities.Availability availability)
	{
		return ReadModel.Dtos.Availability.Create(availability._beerId, availability._beerName, availability._quantity);
	}
}