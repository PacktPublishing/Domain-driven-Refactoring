using BrewUp.Shared.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Muflone.Eventstore;
using Muflone.Eventstore.Persistence;

namespace BrewUp.Warehouses.Infrastructures.MongoDb.Readmodel;

public class EventStorePositionRepository : IEventStorePositionRepository
{
	private readonly IMongoDatabase _database;
	private readonly ILogger<EventStorePositionRepository> _logger;

	public EventStorePositionRepository(ILogger<EventStorePositionRepository> logger, MongoDbSettings mongoDbSettings)
	{
		_logger = logger;
		var client = new MongoClient(mongoDbSettings.ConnectionString);
		_database = client.GetDatabase(mongoDbSettings.DatabaseName);
	}

	public async Task<IEventStorePosition> GetLastPosition()
	{
		try
		{
			var collection = _database.GetCollection<LastEventPosition>(nameof(LastEventPosition));
			var filter = Builders<LastEventPosition>.Filter.Eq("_id", Constants.LastEventPositionKey);
			var result = await collection.CountDocumentsAsync(filter) > 0
				? (await collection.FindAsync(filter)).First()
				: null;
			if (result == null)
			{
				result = new LastEventPosition
				{ Id = Constants.LastEventPositionKey, CommitPosition = -1, PreparePosition = -1 };
				await collection.InsertOneAsync(result);
			}

			return new EventStorePosition(result.CommitPosition, result.PreparePosition);
		}
		catch (Exception ex)
		{
			_logger.LogError(
				$"EventStorePositionRepository: Error getting LastSavedPostion, Message: {ex.Message}, StackTrace: {ex.StackTrace}");
			throw;
		}
	}

	public async Task Save(IEventStorePosition position)
	{
		try
		{
			var collection = _database.GetCollection<LastEventPosition>(typeof(LastEventPosition).Name);
			var filter = Builders<LastEventPosition>.Filter.Eq("_id", Constants.LastEventPositionKey);
			var entity = await collection.Find(filter).FirstOrDefaultAsync();
			if (entity == null)
			{
				entity = new LastEventPosition
				{
					Id = Constants.LastEventPositionKey,
					CommitPosition = position.CommitPosition,
					PreparePosition = position.PreparePosition
				};
				await collection.InsertOneAsync(entity);
			}
			else
			{
				if (position.CommitPosition > entity.CommitPosition && position.PreparePosition > entity.PreparePosition)
				{
					entity.CommitPosition = position.CommitPosition;
					entity.PreparePosition = position.PreparePosition;
					await collection.FindOneAndReplaceAsync(filter, entity);
				}
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(
				$"EventStorePositionRepository: Error while updating commit position: {ex.Message}, StackTrace: {ex.StackTrace}");
			throw;
		}
	}
}