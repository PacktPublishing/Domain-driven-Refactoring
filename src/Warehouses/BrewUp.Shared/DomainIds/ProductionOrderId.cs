using Muflone.Core;

namespace BrewUp.Shared.DomainIds;

public sealed class ProductionOrderId : DomainId
{
    public ProductionOrderId(Guid value) : base(value)
    {
    }
}