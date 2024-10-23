using BrewUp.DomainModel.Services;
using BrewUp.Shared.Contracts;
using BrewUp.Shared.CustomTypes;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BrewUp.Rest.Services
{
	public static class WarehousesService
	{
		public static async Task<Ok> HandleSetAvailabilities(SetAvailabilityJson body, IWarehouseService warehousesDomainService, CancellationToken cancellationToken)
		{
			await warehousesDomainService.UpdateAvailabilityDueToProductionOrderAsync(new BeerId(new Guid(body.BeerId)), new BeerName(body.BeerName), body.Quantity, cancellationToken);
			return TypedResults.Ok();
		}
	}
}
