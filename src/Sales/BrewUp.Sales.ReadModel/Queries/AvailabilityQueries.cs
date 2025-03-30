using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class AvailabilityQueries(IMongoClient mongoClient) : IQueries<Availability>
{
	private readonly IMongoDatabase _database = mongoClient.GetDatabase("Sales");

	public async Task<Availability> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		var collection = _database.GetCollection<Availability>(nameof(Availability));
		var filter = Builders<Availability>.Filter.Eq("_id", id);
		return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
			? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
			: null)!;
	}

	public async Task<PagedResult<Availability>> GetByFilterAsync(Expression<Func<Availability, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
	{
		if (--page < 0)
			page = 0;

		var collection = _database.GetCollection<Availability>(nameof(Availability));
		var queryable = query != null
			? collection.AsQueryable().Where(query)
			: collection.AsQueryable();

		var count = await queryable.CountAsync(cancellationToken: cancellationToken);
		var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

		return new PagedResult<Availability>(results, page, pageSize, count);
	}
}