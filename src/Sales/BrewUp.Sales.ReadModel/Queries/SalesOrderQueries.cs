using BrewUp.Sales.ReadModel.Dtos;
using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BrewUp.Sales.ReadModel.Queries;

public sealed class SalesOrderQueries(IMongoClient mongoClient) : IQueries<SalesOrder>
{
	private readonly IMongoDatabase _database = mongoClient.GetDatabase("sales");

	public async Task<SalesOrder> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		var collection = _database.GetCollection<SalesOrder>(nameof(SalesOrder));
		var filter = Builders<SalesOrder>.Filter.Eq("_id", id);
		return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
			? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
			: null)!;
	}

	public async Task<PagedResult<SalesOrder>> GetByFilterAsync(Expression<Func<SalesOrder, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
	{
		if (--page < 0)
			page = 0;

		var collection = _database.GetCollection<SalesOrder>(nameof(SalesOrder));
		var queryable = query != null
			? collection.AsQueryable().Where(query)
			: collection.AsQueryable();

		var count = await queryable.CountAsync(cancellationToken: cancellationToken);
		var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

		return new PagedResult<SalesOrder>(results, page, pageSize, count);
	}
}