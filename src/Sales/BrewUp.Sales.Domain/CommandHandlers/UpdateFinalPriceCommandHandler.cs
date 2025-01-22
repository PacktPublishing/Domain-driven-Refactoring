using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.SharedKernel.Commands;
using BrewUp.Sales.SharedKernel.Events;
using BrewUp.Shared.CustomTypes;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain.CommandHandlers;

public class UpdateFinalPriceCommandHandler : CommandHandlerAsync<UpdateFinalPrice>
{
    public UpdateFinalPriceCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {

    }

    public override async Task HandleAsync(UpdateFinalPrice command, CancellationToken cancellationToken = new CancellationToken())
    {
        var aggregate = await Repository.GetByIdAsync<SalesOrder>(command.SalesOrderId, cancellationToken);
        aggregate.UpdateFinalPrice(command.Price, command.MessageId);
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }

    private void Apply(FinalPriceUpdated @event)
    {
        
    }
}