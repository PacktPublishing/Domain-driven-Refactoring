using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BrewUp.Warehouses.Infrastructures.MongoDb.Readmodel;

public abstract class Queries<T>(IMongoClient mongoClient) : IQueries<T> where T : EntityBase
{
	protected IMongoDatabase Database = mongoClient.GetDatabase("Sales");

	public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		var collection = Database.GetCollection<T>(typeof(T).Name);
		var filter = Builders<T>.Filter.Eq("_id", id);
		return (await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken) > 0
			? (await collection.FindAsync(filter, cancellationToken: cancellationToken)).First()
			: null)!;
	}

	public async Task<PagedResult<T>> GetByFilterAsync(Expression<Func<T, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		if (--page < 0)
			page = 0;

		var collection = Database.GetCollection<T>(typeof(T).Name);
		var queryable = query != null
			? collection.AsQueryable().Where(query)
			: collection.AsQueryable();

		var count = await queryable.CountAsync();
		var results = await queryable.Skip(page * pageSize).Take(pageSize).ToListAsync();

		return new PagedResult<T>(results, page, pageSize, count);
	}
}