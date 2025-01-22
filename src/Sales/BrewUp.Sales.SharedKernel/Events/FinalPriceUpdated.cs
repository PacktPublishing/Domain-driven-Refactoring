using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUp.Sales.SharedKernel.Events;

public sealed class FinalPriceUpdated(SalesOrderId aggregateId, Guid commitId, Price price)
    : DomainEvent(aggregateId, commitId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly Price Price = price;
}