using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BrewUp.Warehouses.Domain.CommandHandlers;

public abstract class CommandHandlerBaseAsync<TCommand> : CommandHandlerAsync<TCommand> where TCommand : class, ICommand
{
	protected CommandHandlerBaseAsync(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(TCommand command, CancellationToken cancellationToken = new())
	{
		try
		{
			Logger.LogInformation(
				"Processing command: {Type} - Aggregate: {CommandAggregateId} - CommandId : {CommandMessageId}",
				command.GetType(), command.AggregateId, command.MessageId);
			await ProcessCommand(command, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(
				"Error processing command: {Type} - Aggregate: {CommandAggregateId} - CommandId : {CommandMessageId} - Messagge: {ExMessage} - Stack Trace {ExStackTrace}",
				command.GetType(), command.AggregateId, command.MessageId, ex.Message, ex.StackTrace);
			throw;
		}
	}

	public abstract Task ProcessCommand(TCommand command, CancellationToken cancellationToken = default);
}