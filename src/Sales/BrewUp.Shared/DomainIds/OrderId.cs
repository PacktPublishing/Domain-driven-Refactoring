using Muflone.Core;

namespace BrewUp.Shared.DomainIds;

public sealed class OrderId : DomainId
{
    public OrderId(Guid value) : base(value)
    {
    }
}