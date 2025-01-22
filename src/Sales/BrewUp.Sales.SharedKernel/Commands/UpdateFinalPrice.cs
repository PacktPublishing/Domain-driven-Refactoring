using BrewUp.Sales.SharedKernel.CustomTypes;
using BrewUp.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.SharedKernel.Commands;

public sealed class UpdateFinalPrice(SalesOrderId aggregateId, Guid commitId, Price price)
    : Command(aggregateId, commitId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly Price Price = price;
}