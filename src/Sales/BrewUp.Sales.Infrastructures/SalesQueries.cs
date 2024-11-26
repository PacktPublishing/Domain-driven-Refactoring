using BrewUp.Shared.Entities;
using BrewUp.Shared.ReadModel;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace BrewUp.Sales.Infrastructures;

public abstract class SalesQueries<T> : IQueries<T> where T : EntityBase
{
	protected readonly IMongoClient MongoClient;
	protected IMongoDatabase Database;

	protected SalesQueries(IMongoClient mongoClient)
	{
		MongoClient = mongoClient;
		Database = MongoClient.GetDatabase("Sales");
	}

	public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken)
	{
		var collection = Database.GetCollection<T>(typeof(T).Name);
		var filter = Builders<T>.Filter.Eq("_id", id);
		return (await collection.CountDocumentsAsync(filter) > 0 ? (await collection.FindAsync(filter)).First() : null)!;
	}

	public async Task<PagedResult<T>> GetByFilterAsync(Expression<Func<T, bool>>? query, int page, int pageSize, CancellationToken cancellationToken)
	{
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