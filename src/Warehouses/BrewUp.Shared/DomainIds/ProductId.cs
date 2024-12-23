using Muflone.Core;

namespace BrewUp.Shared.DomainIds;

public sealed class ProductId : DomainId
{
    public ProductId(Guid value) : base(value)
    {
    }
}