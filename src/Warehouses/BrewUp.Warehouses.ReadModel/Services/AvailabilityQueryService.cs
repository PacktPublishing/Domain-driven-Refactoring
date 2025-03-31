using BrewUp.Shared.Contracts;
using BrewUp.Shared.DomainModel;
using BrewUp.Shared.ReadModel;
using BrewUp.Warehouses.SharedKernel.Entities;
using Microsoft.Extensions.Logging;

namespace BrewUp.Warehouses.ReadModel.Services;

public sealed class AvailabilityQueryService(ILoggerFactory loggerFactory, IQueries<Availability> queries) : ServiceBase(loggerFactory), IAvailabilityQueryService
{
	public async Task<PagedResult<BeerAvailabilityJson>> GetAvailabilityAsync(Guid beerId, CancellationToken cancellationToken)
	{
		try
		{
			var availability = await queries.GetByIdAsync(beerId.ToString(), cancellationToken);
			if (availability == null)			
				return new PagedResult<BeerAvailabilityJson>(Enumerable.Empty<BeerAvailabilityJson>(), 0, 0, 0);
			return new PagedResult<BeerAvailabilityJson>(new[] { availability.ToJson() }, 0, 1, 1);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}